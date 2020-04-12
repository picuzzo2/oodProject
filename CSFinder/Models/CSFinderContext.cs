using Microsoft.EntityFrameworkCore;


namespace CSFinder.Models
{
    public class CSFinderContext : DbContext
    {
        public CSFinderContext()
        { }

        public CSFinderContext(DbContextOptions<CSFinderContext> options) : base(options)
        { }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Matching> Matchings { get; set; }

        public virtual DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Matching>()
                .HasKey(e => new { e.SID, e.CID });
        }

    }
}
