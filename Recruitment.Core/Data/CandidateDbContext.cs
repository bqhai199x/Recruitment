using System.Data.Entity;

namespace Recruitment.Core
{
    public class CandidateDbContext : DbContext
    {
        public CandidateDbContext()
            : base("name=CandidateDbContext")
        {
        }

        public virtual DbSet<Candidate> Candidate { get; set; }
        public virtual DbSet<Level> Level { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
