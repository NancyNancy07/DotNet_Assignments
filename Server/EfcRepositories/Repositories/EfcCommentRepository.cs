using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace EfcRepositories.Repositories;

public class EfcCommentRepository : ICommentRepository
{
    private readonly AppDbContext ctx;

    public EfcCommentRepository(AppDbContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
        await ctx.Comments.AddAsync(comment);
        await ctx.SaveChangesAsync(); 
        return comment;
    }

    public async Task DeleteAsync(int id)
    {
       Comment? existing = await ctx.Comments.SingleOrDefaultAsync(c => c.CommentId == id);
         
        if (existing == null) 
        { 
            throw new Exception($"Comment with id {id} not found");
        }
        
         ctx.Comments.Remove(existing); await ctx.SaveChangesAsync();
    }

    public IQueryable<Comment> GetMany()
    {
        return ctx.Comments.AsQueryable();
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
        Comment? comment = await ctx.Comments
            .SingleOrDefaultAsync(c => c.CommentId == id);

        if (comment == null)
            throw new Exception($"Comment with id {id} not found");

        return comment;
    }

    public async Task UpdateAsync(Comment comment)
    {
      if (!await ctx.Comments.AnyAsync(c => c.CommentId == comment.CommentId)) 
     {
        throw new Exception($"Comment with id {comment.CommentId} not found");
    }
        ctx.Comments.Update(comment);
        await ctx.SaveChangesAsync();
    }
}
