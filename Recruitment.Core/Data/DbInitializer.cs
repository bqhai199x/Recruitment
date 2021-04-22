using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Recruitment.Core
{
    public class DbInitializer : CreateDatabaseIfNotExists<CandidateDbContext>
    {
        protected override void Seed(CandidateDbContext context)
        {
            //Position
            var positions = new List<Position>()
            {
                new Position()
                {
                    PositionName = "C#"
                },
                new Position()
                {
                    PositionName = "Java"
                },
                new Position()
                {
                    PositionName = "PHP"
                },
                new Position()
                {
                    PositionName = "NodeJs"
                }
            };
            context.Position.AddRange(positions);

            //Level
            var levels = new List<Level>()
            {
                new Level()
                {
                    LevelName = "Internship"
                },
                new Level()
                {
                    LevelName = "Fresher"
                },
                new Level()
                {
                    LevelName = "Junior"
                },
                new Level()
                {
                    LevelName = "Senior"
                }
            };
            context.Level.AddRange(levels);

            //Employee
            var employees = new List<Employee>()
            {
                new Employee()
                {
                    FullName = "Vũ Mạnh Dương",
                    Email = "bqhai.199x@gmail.com"
                },
                new Employee()
                {
                    FullName = "Vũ Trung Hiếu",
                    Email = "hieuvt@saisystem.vn"
                },
                new Employee()
                {
                    FullName = "Cung Đình Tùng",
                    Email = "tungcd@saisystem.vn"
                },
                new Employee()
                {
                    FullName = "Nguyễn Văn Tường",
                    Email = "tuongnv@saisystem.vn"
                }
            };
            context.Employee.AddRange(employees);

            //Candidate
            var candidates = new List<Candidate>()
            {
                new Candidate()
                {
                    Position = positions.FirstOrDefault(s => s.PositionName.Equals("C#")),
                    Level = levels.FirstOrDefault(s => s.LevelName.Equals("Internship")),
                    FullName = "Bùi Quang Hải",
                    Birthday = "10/11/1999",
                    Address = "Hà Nội",
                    Phone = "0976445870",
                    Email = "haibq@saisystem.vn",
                    IntroduceName = null,
                    CV = "Nguyen-Duy-Hanh.pdf",
                    IsPDF = true
                },
                new Candidate()
                {
                    Position = positions.FirstOrDefault(s => s.PositionName.Equals("C#")),
                    Level = levels.FirstOrDefault(s => s.LevelName.Equals("Fresher")),
                    FullName = "Nguyễn Duy Tín",
                    Birthday = "12/01/1999",
                    Address = "Thái Bình",
                    Phone = "0972493540",
                    Email = "tinnd@saisystem.vn",
                    IntroduceName = null,
                    CV = "Dang-Thanh-Hieu.pdf",
                    IsPDF = true
                },
                new Candidate()
                {
                    Position = positions.FirstOrDefault(s => s.PositionName.Equals("Java")),
                    Level = levels.FirstOrDefault(s => s.LevelName.Equals("Junior")),
                    FullName = "Lương Đình Nam",
                    Birthday = "14/09/1999",
                    Address = "Vĩnh Phúc",
                    Phone = "0968254196",
                    Email = "namld@saisystem.vn",
                    IntroduceName = null,
                    CV = "Tran-Ngoc-Truong-CV.pdf",
                    IsPDF = true
                },
                new Candidate()
                {
                    Position = positions.FirstOrDefault(s => s.PositionName.Equals("PHP")),
                    Level = levels.FirstOrDefault(s => s.LevelName.Equals("Senior")),
                    FullName = "Trần Thiên Điệp",
                    Birthday = "23/07/1999",
                    Address = "Quảng Ninh",
                    Phone = "0927863541",
                    Email = "dieptt@saisystem.vn",
                    IntroduceName = "Bùi Quang Hải",
                    CV = "CV_FRESHER_DucThinh.pdf",
                    IsPDF = true
                },
                new Candidate()
                {
                    Position = positions.FirstOrDefault(s => s.PositionName.Equals("PHP")),
                    Level = levels.FirstOrDefault(s => s.LevelName.Equals("Fresher")),
                    FullName = "Nguyễn Văn Luân",
                    Birthday = "09/12/1999",
                    Address = "Hải Dương",
                    Phone = "0971269852",
                    Email = "luannv@saisystem.vn",
                    IntroduceName = "Bùi Quang Hải",
                    CV = "CM_Schedule.xlsx",
                    IsPDF = false
                },
                new Candidate()
                {
                    Position = positions.FirstOrDefault(s => s.PositionName.Equals("NodeJs")),
                    Level = levels.FirstOrDefault(s => s.LevelName.Equals("Internship")),
                    FullName = "Cao Đức Anh Vũ",
                    Birthday = "26/01/1999",
                    Address = "Hải Phòng",
                    Phone = "0945896217",
                    Email = "vucda@saisystem.vn",
                    IntroduceName = "Nguyễn Văn Luân",
                    CV = "CM_LogicDesign.xlsx",
                    IsPDF = false
                }
            };
            context.Candidate.AddRange(candidates);

            context.SaveChanges();
        }
    }
}
