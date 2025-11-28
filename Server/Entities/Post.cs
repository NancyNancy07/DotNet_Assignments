using System;

namespace Entities;

public class Post
{
    private Post() { }
    public Post(string title, string body, int userId)
    {
        Title = title;
        Body = body;
        UserId = userId;
    }

    public int PostId { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }
    public int UserId { get; set; }

    public List<Comment> Comments { get; set; } = new ();
    public User User { get; set; } = null!;

}
