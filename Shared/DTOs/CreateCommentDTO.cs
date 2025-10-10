using System;

namespace DTOs;

public class CreateCommentDTO
{
    public required string Body { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }
}
