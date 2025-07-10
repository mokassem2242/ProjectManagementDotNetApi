using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ProjectManagementApp.Application.Contracts.Infrastructure.identity;
using ProjectManagementApp.Domain.Common;
using ProjectManagementApp.Domain.Entities;
using ProjectManagementApp.Infrastructure.Identity.Models;


namespace ProjectManagementApp.Infrastructure.Persistence
{
    public class ProjectManagementDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ICurrentUserService _currentUserService { get; }
        public IHostEnvironment _env { get; }
        public DbSet<MainProject> Projects { get; set; }
        public DbSet<ProjectTask> Tasks { get; set; }
        public DbSet<DomainUser> Users { get; set; }

        public ProjectManagementDbContext(ICurrentUserService currentUserService, DbContextOptions<ProjectManagementDbContext> options, IHostEnvironment env)
            : base(options)
        {
            _currentUserService = currentUserService;
            _env = env;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Global query filters for soft delete
            modelBuilder.Entity<MainProject>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<ProjectTask>().HasQueryFilter(t => !t.IsDeleted);

            // MainProject-DomainUser (many-to-one)
            modelBuilder.Entity<MainProject>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // MainProject-ProjectTask (one-to-many)
            modelBuilder.Entity<MainProject>()
                .HasMany(p => p.MainTasks)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<DomainUser>()
              .HasIndex(d => d.ApplicationUserId)
              .IsUnique();

            modelBuilder.Entity<IdentityRole<Guid>>().HasData(
             new IdentityRole<Guid>
             {
                 Id = Guid.Parse("9cec1b6d-728c-4c96-9ee2-6a04aad60e8e"),
                 Name = "User",
                 NormalizedName = "USER",
                 ConcurrencyStamp = "9cec1b6d-728c-4c96-9ee2-6a04aad60e8e"
             }
             );


        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = _currentUserService.GetUserId();
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = _currentUserService.GetUserId();
                        break;
                    case EntityState.Deleted:
                        entry.Property("IsDeleted").CurrentValue = true;
                        entry.State = EntityState.Modified;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
