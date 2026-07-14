using TaskApp.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace TaskApp.Data.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Entities.Models.Task> Tasks { get; set; }
        public DbSet<StatusCatalogItem> StatusCatalogItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("tbl_Users");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(250);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);

                entity.Property(e => e.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(e => e.CreatedAt).IsRequired().HasDefaultValueSql("SYSUTCDATETIME()");
                entity.Property(e => e.UpdatedAt).IsRequired().HasDefaultValueSql<DateTime>("SYSUTCDATETIME()");

                entity.HasMany(e => e.Tasks)
                      .WithOne(t => t.AssignedTo)
                      .HasForeignKey(t => t.AssignedToId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasData(

                    );
            });

            modelBuilder.Entity<Entities.Models.Task>(entity =>
            {
                entity.ToTable("tbl_Tasks");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(250);
                entity.Property(e => e.DueDate).IsRequired();
                entity.Property(e => e.StatusId).IsRequired();

                entity.HasOne(Task => Task.AssignedTo)
                      .WithMany(User => User.Tasks)
                      .HasForeignKey(Task => Task.AssignedToId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(Task => Task.Status)
                      .WithMany()
                      .HasForeignKey(Task => Task.StatusId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(e => e.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(e => e.CreatedAt).IsRequired().HasDefaultValueSql("SYSUTCDATETIME()");
                entity.Property(e => e.UpdatedAt).IsRequired().HasDefaultValueSql<DateTime>("SYSUTCDATETIME()");
            });

            modelBuilder.Entity<StatusCatalogItem>(entity =>
            {
                entity.ToTable("tbl_StatusCatalogItems");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(250);

                entity.Property(e => e.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(e => e.CreatedAt).IsRequired().HasDefaultValueSql("SYSUTCDATETIME()");
                entity.Property(e => e.UpdatedAt).IsRequired().HasDefaultValueSql<DateTime>("SYSUTCDATETIME()");
            });
        }
    }
}
