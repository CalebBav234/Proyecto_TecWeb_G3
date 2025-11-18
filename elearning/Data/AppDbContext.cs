using DTOs;
using Microsoft.EntityFrameworkCore;
namespace Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<User> Users => Set<User>();
    public DbSet<Profile> Profiles => Set<Profile>();

    public DbSet<Course> Courses => Set<Course>();

    public DbSet<Lesson> Lessons => Set<Lesson>();

    public DbSet<Enrollment> Enrollments => Set<Enrollment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
         modelBuilder.Entity<User>()
            .HasOne(u => u.Profile)
            .WithOne(p => p.User)
            .HasForeignKey<Profile>(p => p.UserId)
            .HasPrincipalKey<User>(u => u.Id)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Course>()
            .HasOne(c => c.Teacher)
            .WithMany(u => u.CoursesTaught)
            .HasForeignKey(c => c.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Lesson>()
            .HasOne(l => l.Course)
            .WithMany(c => c.Lessons)
            .HasForeignKey(l => l.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Enrollment>()
            .HasKey(e => new { e.UserId, e.CourseId });

        modelBuilder.Entity<Profile>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Course>()
            .Property(c => c.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Lesson>()
            .Property(l => l.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.User)
            .WithMany(u => u.Enrollments)
            .HasForeignKey(e => e.UserId);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Course)
            .WithMany(c => c.Enrollments)
            .HasForeignKey(e => e.CourseId);

        
        var adminUserId = Guid.Parse("00000000-0000-0000-0000-000000000001");
        var hashedPassword = "$2a$11$sMzqb4cfyrk8qpCy.M5Y9OsdNOZxwIjGMYji6OpCAa7LAdBg9B50G";
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = adminUserId,
            Email = "admin@elearning.com",
            Username = "admin",
            PasswordHash = hashedPassword,
            Role = "Admin"
        });

        modelBuilder.Entity<Profile>().HasData(new Profile
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
            UserId = adminUserId,
            FullName = "Administrator",
            Bio = "System Administrator"
        });

    }
}
