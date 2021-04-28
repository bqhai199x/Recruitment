using System;

namespace Recruitment.WebAPI.ViewModels
{
    public class CandidateVM
    {
        public int CandidateId { get; set; }

        public int? PositionId { get; set; }

        public int? LevelId { get; set; }

        public int? EmployeeId { get; set; }

        public string FullName { get; set; }

        public string Birthday { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string CV { get; set; }

        public bool IsPDF { get; set; }

        public string IntroduceName { get; set; }

        public string InterviewName { get; set; }

        public string LevelName { get; set; }

        public string PositionName { get; set; }

        public int? IsApplied { get; set; }

        public bool? IsContacted { get; set; }

        public DateTime? InterviewTime { get; set; }

        public string InterviewLocation { get; set; }

        public string Note { get; set; }

        public int? TestPoint { get; set; }

        public int Status { get; set; }
    }
}