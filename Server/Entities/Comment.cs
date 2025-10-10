using System;

namespace Entities;

public class Comment
{
    public int CommentId { get; set; }
    public string? Body { get; set; }
    public int Userid { get; set; }
    public int Postid { get; set; }

}
