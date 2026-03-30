using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Entities.DTO;
//using SchoolProject.Domain.Entities.DTO;

namespace SchoolProject.Infrastructure.Persistance
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Class> Class {  get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Major> Majors { get; set; }

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
            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.SubscriptionPlanId).IsRequired().HasMaxLength(50);
                entity.HasOne(s => s.Tenant)
                    .WithMany(t => t.Subscriptions)
                    .HasForeignKey(s => s.TenantId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(s => s.SubscriptionPlan)
                      .WithMany()
                      .HasForeignKey(s => s.SubscriptionPlanId)
                      .OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.Entity<SubscriptionPlan>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Price).IsRequired().HasColumnType("decimal(18,2)");
            });
            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(c => c.GradeLevel)
                    .HasMaxLength(10);

                entity.Property(c => c.AcademicYear)
                    .HasMaxLength(20);

                // Relasi ke Enrollment
                entity.HasMany(c => c.Enrollments)
                    .WithOne(e => e.Class)
                    .HasForeignKey(e => e.ClassId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Relasi ke Schedule
                entity.HasMany(c => c.Schedules)
                    .WithOne(s => s.Class)
                    .HasForeignKey(s => s.ClassId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Index untuk multitenant
                entity.HasIndex(c => c.TenantId);
            });
            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.Property(s => s.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(s => s.Code)
                    .HasMaxLength(20);

                // Relasi ke Schedule
                entity.HasMany(s => s.Schedules)
                    .WithOne(sc => sc.Subject)
                    .HasForeignKey(sc => sc.SubjectId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(s => s.TenantId);
            });
            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.Property(s => s.Day)
                    .IsRequired();

                entity.Property(s => s.StartTime)
                    .IsRequired();

                entity.Property(s => s.EndTime)
                    .IsRequired();

                // Relasi ke Class
                entity.HasOne(s => s.Class)
                    .WithMany(c => c.Schedules)
                    .HasForeignKey(s => s.ClassId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Relasi ke Subject
                entity.HasOne(s => s.Subject)
                    .WithMany(sub => sub.Schedules)
                    .HasForeignKey(s => s.SubjectId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(s => new { s.ClassId, s.Day, s.StartTime });

                entity.HasIndex(s => s.TenantId);
            });
            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Status)
                    .HasMaxLength(20);

                entity.Property(e => e.AcademicYear)
                    .HasMaxLength(20);

                // Relasi ke Student
                entity.HasOne(e => e.Student)
                    .WithMany(s => s.Enrollments)
                    .HasForeignKey(e => e.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Relasi ke Class
                entity.HasOne(e => e.Class)
                    .WithMany(c => c.Enrollments)
                    .HasForeignKey(e => e.ClassId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Prevent duplicate enrollment
                entity.HasIndex(e => new { e.StudentId, e.ClassId })
                    .IsUnique();

                entity.HasIndex(e => e.TenantId);
            });
            modelBuilder.Entity<Major>(entity =>
            {
                entity.HasKey(m => m.Id);

                entity.Property(m => m.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(m => m.Code)
                    .HasMaxLength(20);

                entity.HasMany(m => m.Classes)
                    .WithOne(c => c.Major)
                    .HasForeignKey(c => c.MajorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(m => m.Subjects)
                    .WithOne(s => s.Major)
                    .HasForeignKey(s => s.MajorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(m => m.TenantId);
            });
            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasKey(t => t.Id);

                entity.Property(t => t.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(t => t.Email)
                    .HasMaxLength(100);

                entity.Property(t => t.EmployeeNumber)
                    .HasMaxLength(50);

                entity.HasIndex(t => t.TenantId);

                entity.HasIndex(t => new { t.TenantId, t.Email })
                    .IsUnique(false);
            });
            modelBuilder.Entity<Class>(entity =>
            {
                // Relasi wali kelas
                entity.HasOne(c => c.HomeroomTeacher)
                    .WithMany(t => t.HomeroomClasses)
                    .HasForeignKey(c => c.HomeroomTeacherId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
            modelBuilder.Entity<Major>(entity =>
            {
                entity.HasKey(m => m.Id);

                entity.Property(m => m.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(m => m.Code)
                    .HasMaxLength(20);

                entity.HasMany(m => m.Classes)
                    .WithOne(c => c.Major)
                    .HasForeignKey(c => c.MajorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(m => m.Subjects)
                    .WithOne(s => s.Major)
                    .HasForeignKey(s => s.MajorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(m => m.TenantId);
            });
        }
    }
}
