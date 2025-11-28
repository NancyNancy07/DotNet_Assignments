using System;

namespace Entities;

public class User
{ 
public User(string username, string password)
    {
        Username = username;
        Password = password;
    }
    private User() { }

    public int UserId { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }

    public List<Post> Posts { get; set; } = new ();

    public List<Comment> Comments { get; set; } = new ();


}