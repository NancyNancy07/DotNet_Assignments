using System;
namespace DTOs;

public class PostDTO
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Body { get; set; }
    public required string AuthorName { get; set; }
    public List<CommentDTO>? Comments { get; set; }
    public int UserId { get; set; }
}
