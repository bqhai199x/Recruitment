using Recruitment.Core;
using System;

namespace Recruitment
{
    public class CandidateViewModel
    {
        public int CandidateId { get; set; }

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

        public bool IsApplied { get; set; }

        public Contact IsContact { get; set; }

        public DateTime? InterviewTime { get; set; }

        public string InterviewLocation { get; set; }

        public string Note { get; set; }

        public int? TestPoint { get; set; }

        public Status Status { get; set; }
    }
}