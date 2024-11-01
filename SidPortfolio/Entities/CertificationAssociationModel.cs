using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace SidPortfolio.Models
{
    public class CertificationAssociationModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CertificationAssociationId { get; set; }
        public string Title { get; set; }
        public string IssuingOrganization { get; set; }
        public string Description { get; set; }
        [ForeignKey("CertificationModel")]
        public int CertificationId {  get; set; }
        public CertificationModel certificationModel { get; set; }  
        public bool ActiveStatus { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
    }
}
