namespace SidPortfolio.DTO
{
    public class ExperienceResponsibilitiesAssociationDto
    {
        public int ExperienceResponsibilitiesAssociationId { get; set; }
        public int ExperienceId { get; set; }
        public string Responsibilities { get; set; }
        public bool ActiveStatus { get; set; }
    }
}
