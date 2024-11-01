namespace SidPortfolio.DTO
{
    public class ProjectDetails
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string Base64Data { get; set; }
        public ProjectDetailAssociation ProjectDetailAssociation { get; set; }
    }
    public class ProjectDetailAssociation
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string GitHubLink { get; set; }
    }
}
