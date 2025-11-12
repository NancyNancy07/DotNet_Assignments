using DTOs;

namespace BlazorApp.Services;

public interface IPostService
{
    Task<IEnumerable<PostDTO>> GetAllAsync();
    Task<PostDTO> GetByIdAsync(int id);
    public Task<PostDTO> AddPostAsync(CreatePostDTO request);
}
