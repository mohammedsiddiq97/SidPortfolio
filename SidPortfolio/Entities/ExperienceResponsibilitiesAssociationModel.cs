using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace SidPortfolio.Models
{
    public class ExperienceResponsibilitiesAssociationModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExperienceResponsibilitiesAssociationId { get; set; }
        [ForeignKey("ExperienceModel")]
        public int ExperienceId { get; set; }
        public ExperienceModel ExperienceModel { get; set; }
        public string Responsibilities { get; set; }
        public bool ActiveStatus { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
    }
}

