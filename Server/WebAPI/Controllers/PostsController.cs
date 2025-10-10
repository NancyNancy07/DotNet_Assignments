using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using Entities;
using DTOs;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository postRepo;

        public PostsController(IPostRepository postRepo)
        {
            this.postRepo = postRepo;
        }

        // Create new post
        [HttpPost]
        public async Task<ActionResult<PostDTO>> AddPost([FromBody] CreatePostDTO request)
        {
            Post post = new(request.Title, request.Body, request.UserId);
            Post created = await postRepo.AddAsync(post);

            PostDTO dto = new()
            {
                Id = created.PostId,
                Title = created.Title ?? string.Empty,
                Body = created.Body ?? string.Empty,
                UserId = created.UserId,
                AuthorName = "Unknown"
            };

            return Created($"/posts/{dto.Id}", dto);
        }

        // Get a single post by Id
        [HttpGet("{postId:int}")]
        public async Task<ActionResult<PostDTO>> GetPostById([FromRoute] int postId)
        {
            Post post = await postRepo.GetSingleAsync(postId);
            if (post == null)
                return NotFound();

            PostDTO dto = new()
            {
                Id = post.PostId,
                Title = post.Title ?? string.Empty,
                Body = post.Body ?? string.Empty,
                UserId = post.UserId,
                AuthorName = "Unknown"
            };

            return Ok(dto);
        }

        // Get many posts
        [HttpGet]
        public async Task<ActionResult<List<PostDTO>>> GetManyPosts([FromQuery] string? titleContains = null)
        {
            IQueryable<Post> posts = postRepo.GetMany();

            if (!string.IsNullOrEmpty(titleContains))
                posts = posts.Where(p => p.Title != null && p.Title.Contains(titleContains, StringComparison.OrdinalIgnoreCase));

            List<PostDTO> dtos = posts.Select(p => new PostDTO
            {
                Id = p.PostId,
                Title = p.Title ?? string.Empty,
                Body = p.Body ?? string.Empty,
                UserId = p.UserId,
                AuthorName = "Unknown" // Replace with actual author name if available
            }).ToList();

            return Ok(dtos);
        }

        // Delete a post
        [HttpDelete("{postId:int}")]
        public async Task<ActionResult<PostDTO>> DeletePost([FromRoute] int postId)
        {
            Post existingPost = await postRepo.GetSingleAsync(postId);
            if (existingPost == null)
                return NotFound();

            await postRepo.DeleteAsync(postId);

            PostDTO dto = new()
            {
                Id = existingPost.PostId,
                Title = existingPost.Title ?? string.Empty,
                Body = existingPost.Body ?? string.Empty,
                UserId = existingPost.UserId,
                AuthorName = "Unknown"
            };

            return Ok(dto);
        }

        // Update an existing post
        [HttpPut("{postId:int}")]
        public async Task<ActionResult<PostDTO>> UpdatePost([FromRoute] int postId, [FromBody] CreatePostDTO request)
        {
            Post existingPost = await postRepo.GetSingleAsync(postId);
            if (existingPost == null)
                return NotFound();

            existingPost.Title = request.Title;
            existingPost.Body = request.Body;
            existingPost.UserId = request.UserId;

            await postRepo.UpdateAsync(existingPost);

            PostDTO dto = new()
            {
                Id = existingPost.PostId,
                Title = existingPost.Title,
                Body = existingPost.Body,
                UserId = existingPost.UserId,
                AuthorName = "Unknown"
            };

            return Ok(dto);
        }
    }
}
