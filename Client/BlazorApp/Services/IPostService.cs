using DTOs;

namespace BlazorApp.Services;

public interface IPostService
{
 public Task<PostDTO> AddUserAsync(CreatePostDTO request); 
}
