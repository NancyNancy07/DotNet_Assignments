using BlazorApp.Components.Pages;
using DTOs;

namespace BlazorApp.Services;

public interface ICommentService
{
 public Task<CommentDTO> AddCommentAsync(int id,CreateCommentDTO request); 
}
