using System;

namespace SidPortfolio.DTO
{
    public class SkillAssessmentDTO
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string SkillAssessmentBase64 { get; set; }
        public bool ActiveStatus { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
    }
}
