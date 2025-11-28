using System;

namespace Entities;

public class Comment
{
    private Comment() { }
    public int CommentId { get; set; }
    public string? Body { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }

    public Post Post { get; set; }

    public User User { get; set; }
}
