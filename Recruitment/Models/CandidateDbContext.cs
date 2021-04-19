using System.Data.Entity;

namespace Recruitment.Models
{
    public partial class CandidateDbContext : DbContext
    {
        public CandidateDbContext()
            : base("name=CandidateDbContext")
        {
        }

        public virtual DbSet<Candidate> Candidate { get; set; }
        public virtual DbSet<Level> Level { get; set; }
        public virtual DbSet<Position> Position { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
