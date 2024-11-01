using Microsoft.AspNetCore.Http;

namespace SidPortfolio.DTO
{
    public class ProjectViewModel
    {
        public IFormFile File { get; set; }
        public string FileTitle { get; set; }
        public string Description { get; set; }
        public string GitHubLink { get; set; }
    }
}
