using System.Collections.Generic;

namespace SidPortfolio.DTO
{
    public class ExperienceViewModel
    {
        public string JobTitle { get; set; }
        public string Company { get; set; }
        public string Duration { get; set; }
      public List<string> Responsibilities { get; set; }    
    }
}
