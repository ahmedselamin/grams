namespace Grams.Server.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    { }
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Like> Likes { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Posts)
            .WithOne()
            .HasForeignKey(p => p.UserId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Likes)
            .WithOne()
            .HasForeignKey(l => l.UserId);

        modelBuilder.Entity<Post>()
            .HasMany(p => p.Likes)
            .WithOne()
            .HasForeignKey(l => l.PostId);
    }
}
