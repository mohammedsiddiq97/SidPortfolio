namespace SidPortfolio.DTO
{
    public class CertificationDto
    {

        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string Base64Data { get; set; }
        public CertificationAssociationDto CertificationAssociation { get; set; }

    }
    public class CertificationAssociationDto
    {
        public string Title { get; set; }
        public string IssuingOrganization { get; set; }
        public string Description { get; set; }
    }
}
