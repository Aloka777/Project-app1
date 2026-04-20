using Microsoft.EntityFrameworkCore;
using Projectapp.Models;

namespace Projectapp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<ResearchArea> ResearchAreas { get; set; }
        public DbSet<MasterKeyword> MasterKeywords { get; set; }
        public DbSet<ProjectProposal> ProjectProposals { get; set; }
        public DbSet<ProjectKeyword> ProjectKeywords { get; set; }
        public DbSet<SupervisorInterest> SupervisorInterests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // FIX: Explicitly define the relationship so .Include() works correctly
            modelBuilder.Entity<ResearchArea>()
                .HasOne(r => r.Faculty)
                .WithMany(f => f.ResearchAreas)
                .HasForeignKey(r => r.FacultyId);
        }
    }
}