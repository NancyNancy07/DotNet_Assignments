using System;

namespace DTOs;

public class CreatePostDTO
{
    public required string Title { get; set; }
    public required string Body { get; set; }
    public int UserId { get; set; }
}
