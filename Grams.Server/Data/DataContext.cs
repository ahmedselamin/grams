namespace Grams.Server.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Images)
            .WithOne()
            .HasForeignKey(i => i.UserId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Posts)
            .WithOne()
            .HasForeignKey(p => p.UserId);
    }
}
