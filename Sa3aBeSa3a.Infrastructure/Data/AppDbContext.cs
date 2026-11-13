using Microsoft.EntityFrameworkCore;
using Sa3aBeSa3a.Domain.Entities;

namespace Sa3aBeSa3a.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public const string ConnectionString = "Server=localhost\\SQLEXPRESS;Database=Sa3aBeSa3aDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Members> Members { get; set; }
        public DbSet<HelpRequests> HelpRequests { get; set; }
        public DbSet<Offers> Offers { get; set; }
        public DbSet<Auditlogs> Auditlogs { get; set; }
        public DbSet<LedgerEntries> LedgerEntries { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<MemberSkills> MemberSkills { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Badges> Badges { get; set; }
        public DbSet<MemberBadges> MemberBadges { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }

        }
    }
}
