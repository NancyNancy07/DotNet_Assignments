using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace EfcRepositories.Repositories;

public class EfcPostRepository : IPostRepository
{
    private readonly AppDbContext ctx;

    public EfcPostRepository(AppDbContext ctx)
    {
        this.ctx = ctx;
    }


    public async Task<Post> AddAsync(Post post)
    {
        await ctx.Posts.AddAsync(post);
        await ctx.SaveChangesAsync(); 
        return post;
    }

    public async Task DeleteAsync(int id)
    {
        Post? existing = await ctx.Posts.SingleOrDefaultAsync(p => p.PostId == id);
         
        if (existing == null) 
        { 
            throw new Exception($"Post with id {id} not found");
        }
        
         ctx.Posts.Remove(existing); await ctx.SaveChangesAsync();
    }

    public  IQueryable<Post> GetMany()
    {
      return ctx.Posts.AsQueryable();
    }

    public async Task<Post> GetSingleAsync(int id)
    {
        Post? post = await ctx.Posts
            .SingleOrDefaultAsync(p => p.PostId == id);

        if (post == null)
            throw new Exception($"Post with id {id} not found");

        return post;
    }

    public async Task UpdateAsync(Post post)
    {
     if (!await ctx.Posts.AnyAsync(p => p.PostId == post.PostId)) 
     {
        throw new Exception("Post with id {post.Id} not found");
    }
        ctx.Posts.Update(post);
        await ctx.SaveChangesAsync();
    }
}
