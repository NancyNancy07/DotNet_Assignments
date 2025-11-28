using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace EfcRepositories.Repositories;

public class EfcUserRepository : IUserRepository
{
    private readonly AppDbContext ctx;

    public EfcUserRepository(AppDbContext ctx)
    {
        this.ctx = ctx;
    }


    public async Task<User> AddAsync(User user)
    {
        await ctx.Users.AddAsync(user);
        await ctx.SaveChangesAsync(); 
        return user;
    }

    public async Task DeleteAsync(int id)
    {
        User? existing = await ctx.Users.SingleOrDefaultAsync(u => u.UserId == id);
         
        if (existing == null) 
        { 
            throw new Exception($"User with id {id} not found");
        }
        
         ctx.Users.Remove(existing); await ctx.SaveChangesAsync();
    }

    public  IQueryable<User> GetMany()
    {
      return ctx.Users.AsQueryable();
    }

    public async Task<User> GetSingleAsync(int id)
    {
        User? user = await ctx.Users
            .SingleOrDefaultAsync(u => u.UserId == id);

        if (user == null)
            throw new Exception($"User with id {id} not found");

        return user;
    }

    public async Task<User> GetSingleByUsernameAsync(string username)
    {  
        if (string.IsNullOrEmpty(username))
            throw new ArgumentException("Username cannot be null or empty.", nameof(username));

        User? user = await ctx.Users
            .SingleOrDefaultAsync(u => u.Username == username);

        if (user == null)
            throw new Exception($"User with username '{username}' not found");

        return user;
    }

    public async Task UpdateAsync(User user)
    {
     if (!await ctx.Users.AnyAsync(u => u.UserId == user.UserId)) 
     {
        throw new Exception($"User with id {user.UserId} not found");
    }
        ctx.Users.Update(user);
        await ctx.SaveChangesAsync();
    }
}
