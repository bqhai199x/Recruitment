namespace Recruitment.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Candidate")]
    public class Candidate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        [ForeignKey("LevelId")]
        public virtual Level Level { get; set; }

        [ForeignKey("PositionId")]
        public virtual Position Position { get; set; }
    }
}
