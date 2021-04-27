using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Recruitment.Core;

namespace Recruitment.WebAPI.Data
{
    public class RecruitmentDbContext : DbContext
    {
        public RecruitmentDbContext (DbContextOptions<RecruitmentDbContext> options)
            : base(options)
        {
        }

        public DbSet<Recruitment.Core.Candidate> Candidate { get; set; }
    }
}
