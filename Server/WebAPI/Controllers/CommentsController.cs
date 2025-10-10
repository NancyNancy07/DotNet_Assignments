using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using Entities;
using DTOs;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository commentRepo;
        private readonly IUserRepository userRepo;

        public CommentsController(ICommentRepository commentRepo, IUserRepository userRepo)
        {
            this.commentRepo = commentRepo;
            this.userRepo = userRepo;
        }

        // Create new comment
        [HttpPost]
        public async Task<ActionResult<CommentDTO>> AddComment([FromBody] CreateCommentDTO request)
        {
            Comment comment = new()
            {
                Body = request.Body,
                Userid = request.UserId,
                Postid = request.PostId
            };
            Comment created = await commentRepo.AddAsync(comment);

            // Get username for DTO
            var user = await userRepo.GetSingleAsync(created.Userid);

            CommentDTO dto = new()
            {
                Id = created.CommentId,
                Body = created.Body ?? string.Empty,
                UserName = user?.Username ?? string.Empty,
                PostId = created.Postid
            };

            return Created($"/comments/{dto.Id}", dto);
        }

        // Get a single comment by Id
        [HttpGet("{commentId:int}")]
        public async Task<ActionResult<CommentDTO>> GetCommentById([FromRoute] int commentId)
        {
            Comment comment = await commentRepo.GetSingleAsync(commentId);
            if (comment == null)
                return NotFound();

            var user = await userRepo.GetSingleAsync(comment.Userid);

            CommentDTO dto = new()
            {
                Id = comment.CommentId,
                Body = comment.Body ?? string.Empty,
                UserName = user?.Username ?? string.Empty,
                PostId = comment.Postid
            };

            return Ok(dto);
        }

        // Get many comments (optionally by PostId or UserId)
        [HttpGet]
        public async Task<ActionResult<List<CommentDTO>>> GetManyComments([FromQuery] int? postId = null, [FromQuery] int? userId = null)
        {
            IQueryable<Comment> comments = commentRepo.GetMany();

            if (postId.HasValue)
                comments = comments.Where(c => c.Postid == postId.Value);

            if (userId.HasValue)
                comments = comments.Where(c => c.Userid == userId.Value);

            List<CommentDTO> dtos = new();
            foreach (var comment in comments)
            {
                var user = await userRepo.GetSingleAsync(comment.Userid);
                dtos.Add(new CommentDTO
                {
                    Id = comment.CommentId,
                    Body = comment.Body ?? string.Empty,
                    UserName = user?.Username ?? string.Empty,
                    PostId = comment.Postid
                });
            }

            return Ok(dtos);
        }

        // Update a comment
        [HttpPut("{commentId:int}")]
        public async Task<ActionResult<CommentDTO>> UpdateComment([FromRoute] int commentId, [FromBody] CreateCommentDTO request)
        {
            Comment existingComment = await commentRepo.GetSingleAsync(commentId);
            if (existingComment == null)
                return NotFound();

            existingComment.Body = request.Body;
            existingComment.Userid = request.UserId;
            existingComment.Postid = request.PostId;

            await commentRepo.UpdateAsync(existingComment);

            var user = await userRepo.GetSingleAsync(existingComment.Userid);

            CommentDTO dto = new()
            {
                Id = existingComment.CommentId,
                Body = existingComment.Body ?? string.Empty,
                UserName = user?.Username ?? string.Empty,
                PostId = existingComment.Postid
            };

            return Ok(dto);
        }

        // Delete a comment
        [HttpDelete("{commentId:int}")]
        public async Task<ActionResult<CommentDTO>> DeleteComment([FromRoute] int commentId)
        {
            Comment existingComment = await commentRepo.GetSingleAsync(commentId);
            if (existingComment == null)
                return NotFound();

            await commentRepo.DeleteAsync(commentId);

            var user = await userRepo.GetSingleAsync(existingComment.Userid);

            CommentDTO dto = new()
            {
                Id = existingComment.CommentId,
                Body = existingComment.Body ?? string.Empty,
                UserName = user?.Username ?? string.Empty,
                PostId = existingComment.Postid
            };

            return Ok(dto);
        }
    }
}
