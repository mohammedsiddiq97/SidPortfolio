using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SidPortfolio.DTO;
using SidPortfolio.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SidPortfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "ApiKeyPolicy")]
    public class ProjectDetailController : ControllerBase
    {
        #region Private Variables
        private readonly IProjectDetailRepository _projectRepository;
        #endregion

        #region Constructor
        public ProjectDetailController(IProjectDetailRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        [ProducesResponseType(typeof(ResponseModel<List<ProjectDetails>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProjectsInfoAsync()
        {
            var projectResponse = await _projectRepository.GetProjectsInfoAsync();
            if (!projectResponse.IsSuccess)
            {
                return BadRequest("Something went wrong while fetching ProjectsInfo Please try again later");
            }
            return Ok(projectResponse);
        }

        [HttpPost("SaveNewProject")]
        [ProducesResponseType(typeof(ResponseModel<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SaveNewProjectAsync(ProjectViewModel projectDetails)
        {
            var saveProjectResponse = await _projectRepository.SaveNewProjectAsync(projectDetails);
            if (!saveProjectResponse.IsSuccess)
            {
                return BadRequest("Something Went Wrong While Saving the ProjectInfo, Please Try Again Later");
            }
            return Ok(saveProjectResponse);
        }
        #endregion
    }
}
