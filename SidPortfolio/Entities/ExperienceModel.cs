using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SidPortfolio.Models
{
    public class ExperienceModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExperienceId { get; set; }
        public string JobTitle { get; set; }
        public string Company { get; set; }
        public string Duration { get; set; }
        public ICollection<ExperienceResponsibilitiesAssociationModel> ExperienceResponsibilitiesAssociation { get; set; }
        public bool ActiveStatus { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
    }
}
