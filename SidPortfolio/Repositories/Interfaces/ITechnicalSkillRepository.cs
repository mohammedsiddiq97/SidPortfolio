using SidPortfolio.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SidPortfolio.Repositories.Interfaces
{
    public interface ITechnicalSkillRepository
    {
        public Task<ResponseModel<List<string>>> GetTechnicalSkillsInfoAsync();
        public Task<ResponseModel<string>> SaveNewTechnicalSkillAsync(string skillName);
    }
}
