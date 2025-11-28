using System;

namespace Entities;

public class Comment
{
    public Comment() { }
     public Comment(string body, int userId, int postId)
    {
        Body = body;
        UserId = userId;
        PostId = postId;
    }
    public int CommentId { get; set; }
    public string? Body { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }

    public Post Post { get; set; } = null!;

    public User User { get; set; } = null!;
}
