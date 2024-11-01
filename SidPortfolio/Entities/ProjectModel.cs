using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace SidPortfolio.Models
{
    public class ProjectModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
        public long Size { get; set; }
        public ProjectAssociationModel ProjectAssociation {  get; set; }    
        public bool ActiveStatus { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
    }
    public class ProjectAssociationModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectAssociationId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [ForeignKey("ProjectId")]
        public ProjectModel ProjectModel { get; set; }
        public int ProjectId { get; set; }
        public string GitHubLink { get; set; }
        public bool ActiveStatus { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
    }
}
