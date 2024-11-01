using System.Collections.Generic;

namespace SidPortfolio.DTO
{
    public class ExperienceDto
    {
        public string JobTitle { get; set; }
        public string Company { get; set; }
        public string Duration { get; set; }
        public ICollection<ExperienceResponsibilitiesAssociationDto> ExperienceAssociations { get; set; }
    }
}
