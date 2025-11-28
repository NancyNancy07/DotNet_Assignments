using Entities;
using Microsoft.EntityFrameworkCore;

namespace EfcRepositories;

public class AppDbContext : DbContext
{
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Comment> Comments => Set<Comment>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"C:\Users\13nan\OneDrive - ViaUC\Software_Eng\3_sem\IT-DNP1X-A25\DotNet_Assignments\Server\EfcRepositories\app.db");
    }
}
