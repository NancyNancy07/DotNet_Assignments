using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class CommentFileRepository : ICommentRepository
{
    private readonly string filePath = "comment.json";

    public CommentFileRepository()
    {
        if (File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    public async Task<Comment> AddAsync(Comment comment)
    {
        string commentAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentAsJson)!;
        int maxId = comments.Count > 0 ? comments.Max(c => c.CommentId) : 1;
        comment.CommentId = maxId + 1;
        comments.Add(comment);
        commentAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentAsJson);
        return comment;

    }

    public async Task DeleteAsync(int id)
    {
        string commentAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentAsJson)!;

        Comment? commentToRemove = comments.SingleOrDefault(c => c.CommentId == id);

        if (commentToRemove is null)
        {
            throw new InvalidOperationException(
               $"Comment with ID '{id}' not found");
        }
        comments.Remove(commentToRemove);
        commentAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentAsJson);
        return;
    }

    public IQueryable<Comment> GetMany()
    {
        string commentAsJson = File.ReadAllTextAsync(filePath).Result;
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentAsJson)!;
        return comments.AsQueryable();
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
        string commentAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentAsJson)!;
        Comment? comment = comments.SingleOrDefault(c => c.CommentId == id);
        if (comment is null)
        {
            throw new InvalidOperationException(
               $"Comment with ID '{id}' not found");
        }

        return comment;
    }

    public async Task UpdateAsync(Comment comment)
    {
        string commentAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentAsJson)!;

        Comment? existingComment = comments.SingleOrDefault(c => c.CommentId == comment.CommentId);
        if (existingComment is null)
        {
            throw new InvalidOperationException(
                $"Comment with ID {comment.CommentId} not found");
        }
        comments.Remove(existingComment);
        comments.Add(comment);
        commentAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentAsJson);
        return;
    }
}
