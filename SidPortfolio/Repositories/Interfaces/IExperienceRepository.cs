using SidPortfolio.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SidPortfolio.Repositories.Interfaces
{
    public interface IExperienceRepository
    {
        public Task<ResponseModel<List<ExperienceDto>>> GetExperienceInfoAsync();
        public Task<ResponseModel<string>> SaveNewExperienceAsync(ExperienceViewModel experience);
    }
}
