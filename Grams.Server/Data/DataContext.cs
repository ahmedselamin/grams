namespace Grams.Server.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    { }
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Comment> Comments { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()     //user and post 
            .HasMany(u => u.Posts)
            .WithOne()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()     //user and notification
            .HasMany(u => u.Notifications)
            .WithOne()
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()     //user and lik
            .HasMany(u => u.Likes)
            .WithOne()
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<User>()  //user and comment
            .HasMany(u => u.Comments)
            .WithOne()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Post>()  //post and comment
           .HasMany(p => p.Comments)
           .WithOne()
           .HasForeignKey(c => c.PostId)
           .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Like>()   // like and post
            .HasOne<Post>()
            .WithMany()
            .HasForeignKey(l => l.PostId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
