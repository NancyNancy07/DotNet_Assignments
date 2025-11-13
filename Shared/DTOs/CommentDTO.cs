using System;

namespace DTOs;

public class CommentDTO
{
    public int Id { get; set; }
    public required string Body { get; set; }
    public string? UserName { get; set; }

    public int UserId { get; set; }
    public int PostId { get; set; }
}
