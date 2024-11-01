using SidPortfolio.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SidPortfolio.Repositories.Interfaces
{
    public interface IProjectDetailRepository
    {
        public Task<ResponseModel<List<ProjectDetails>>> GetProjectsInfoAsync();
        public Task<ResponseModel<string>> SaveNewProjectAsync(ProjectViewModel projectDetails);
    }
}
