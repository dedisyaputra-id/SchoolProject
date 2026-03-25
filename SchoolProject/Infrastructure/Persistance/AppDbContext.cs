using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Infrastructure.Persistance
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasOne<Tenant>()
                      .WithMany()
                      .HasForeignKey(e => e.TenantId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Tenant>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.Role).IsRequired().HasMaxLength(20);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasOne<Tenant>()
                      .WithMany()
                      .HasForeignKey(e => e.TenantId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
