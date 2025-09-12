using System;

namespace Entities;

public class Post
{
    public int PostId { get; set; }
    public required string Title { get; set; }
    public required string Body { get; set; }
    public int UserId { get; set; }


}
