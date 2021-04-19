using Recruitment.Core;
using System.Data.Entity;

namespace Recruitment.WebMVC.App_Start
{
    public class DatabaseSetup
    {
        public static void Initialize()
        {
            Database.SetInitializer(new DbInitializer());
            using (var db = new CandidateDbContext())
            {
                db.Database.Initialize(true);
            }
        }
    }
}