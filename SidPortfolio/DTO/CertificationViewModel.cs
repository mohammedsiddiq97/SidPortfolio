using Microsoft.AspNetCore.Http;


namespace SidPortfolio.DTO
{
    public class CertificationViewModel
    {
        public IFormFile File { get; set; }
        public string FileTitle {  get; set; }
        public string IssuingOrganization { get; set; }
        public string Description {  get; set; }
    }
}
