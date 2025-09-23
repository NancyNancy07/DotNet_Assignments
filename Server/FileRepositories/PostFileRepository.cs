using System;
using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class PostFileRepository : IPostRepository
{
    private readonly string filePath = "post.json";

    public PostFileRepository()
    {
        if (!File.Exists(filePath) || new FileInfo(filePath).Length == 0)
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    public async Task<Post> AddAsync(Post post)
    {
        string postAsJson = await File.ReadAllTextAsync(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postAsJson)!;

        int maxId = posts.Count > 0 ? posts.Max(p => p.PostId) : 1;
        post.PostId = maxId + 1;
        posts.Add(post);
        postAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filePath, postAsJson);
        return post;
    }

    public async Task DeleteAsync(int id)
    {
        string postAsJson = await File.ReadAllTextAsync(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postAsJson)!;

        Post? postToRemove = posts.SingleOrDefault(p => p.PostId == id);

        if (postToRemove is null)
        {
            throw new InvalidOperationException(
               $"Post with ID '{id}' not found");
        }
        posts.Remove(postToRemove);
        postAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filePath, postAsJson);
        return;
    }

    public IQueryable<Post> GetMany()
    {
        string postAsJson = File.ReadAllTextAsync(filePath).Result;
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postAsJson)!;
        return posts.AsQueryable();

    }

    public async Task<Post> GetSingleAsync(int id)
    {
        string postAsJson = await File.ReadAllTextAsync(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postAsJson)!;

        Post? post = posts.SingleOrDefault(p => p.PostId == id);

        if (post is null)
        {
            throw new InvalidOperationException(
               $"Post with ID '{id}' not found");
        }
        return post;
    }

    public async Task UpdateAsync(Post post)
    {
        string postAsJson = await File.ReadAllTextAsync(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postAsJson)!;

        Post? exisitngPost = posts.SingleOrDefault(p => p.PostId == post.PostId);
        if (exisitngPost is null)
        {
            throw new InvalidOperationException(
                $"Post with ID {post.PostId} not found");
        }

        posts.Remove(exisitngPost);
        posts.Add(post);
        postAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filePath, postAsJson);
        return;
    }
}
