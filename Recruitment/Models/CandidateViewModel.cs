namespace Recruitment.Models
{
    public class CandidateViewModel
    {
        public int EmployeeId { get; set; }

        public int? PositionId { get; set; }

        public int? LevelId { get; set; }

        public string FullName { get; set; }

        public string Birthday { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string CV { get; set; }

        public string IntroduceName { get; set; }

        public string LevelName { get; set; }

        public string PositionName { get; set; }
    }
}