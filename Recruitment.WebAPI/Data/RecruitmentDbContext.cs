using Microsoft.EntityFrameworkCore;

namespace Recruitment.WebAPI.Data
{
    public class RecruitmentDbContext : DbContext
    {
        public RecruitmentDbContext(DbContextOptions<RecruitmentDbContext> options)
            : base(options)
        {
        }
        public DbSet<Core.Candidate> Candidate { get; set; }
        public DbSet<Core.Employee> Employee { get; set; }
        public DbSet<Core.Level> Level { get; set; }
        public DbSet<Core.Position> Position { get; set; }
    }
}
