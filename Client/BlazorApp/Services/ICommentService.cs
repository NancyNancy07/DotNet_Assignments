using DTOs;

namespace BlazorApp.Services;

public interface ICommentService
{
 public Task<CommentDTO> AddUserAsync(CreateCommentDTO request); 
}
