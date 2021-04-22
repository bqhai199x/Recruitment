namespace Recruitment.Core
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Candidate")]
    public class Candidate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        public int? IsApplied { get; set; }

        public bool? IsContacted { get; set; }

        public DateTime? InterviewTime { get; set; }

        public string InterviewLocation { get; set; }

        public string Note { get; set; }

        public int? TestPoint { get; set; }

        public int Status { get; set; }

        [ForeignKey("LevelId")]
        public virtual Level Level { get; set; }

        [ForeignKey("PositionId")]
        public virtual Position Position { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}
