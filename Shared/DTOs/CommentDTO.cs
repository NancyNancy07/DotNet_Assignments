using System;

namespace DTOs;

public class CommentDTO
{
    public int Id { get; set; }
    public required string Body { get; set; }
    public required string UserName { get; set; }
    public int PostId { get; set; }
}
