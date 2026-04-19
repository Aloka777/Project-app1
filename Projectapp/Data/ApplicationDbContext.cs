using Microsoft.EntityFrameworkCore;
using Projectapp.Models; 

namespace Projectapp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProjectProposal> ProjectProposals { get; set; }

        public DbSet<ApplicationUser> Users { get; set; }
    }
}