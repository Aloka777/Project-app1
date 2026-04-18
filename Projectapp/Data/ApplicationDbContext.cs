using Microsoft.EntityFrameworkCore;
using Projectapp.Models;
using Projectapp.Models; // This lets the bridge see your ProjectProposal file

namespace Projectapp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // This line creates the "ProjectProposals" table in SQL
        public DbSet<ProjectProposal> ProjectProposals { get; set; }

        public DbSet<ApplicationUser> Users { get; set; }

        public DbSet<Keyword> Keywords { get; set; }
    }
}