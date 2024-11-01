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
    public class ExperienceController : ControllerBase
    {
        #region Private Variables
        private readonly IExperienceRepository _experienceRepository;
        #endregion

        #region Constructor
        public ExperienceController(IExperienceRepository experienceRepository)
        {
            _experienceRepository = experienceRepository;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        [ProducesResponseType(typeof(ResponseModel<List<ExperienceDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetExperienceInfoAsync()
        
        {
            var experienceResponse = await _experienceRepository.GetExperienceInfoAsync();
            if(!experienceResponse.IsSuccess)
            {
                return BadRequest("Something Went Wrong Please Try Again Later");
            }
            return Ok(experienceResponse);
           
            
        }
        [HttpPost("SaveNewExperience")]
        [ProducesResponseType(typeof(ResponseModel<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SaveNewExperienceAsync(ExperienceViewModel experience)
        {
            if (experience == null || experience.Responsibilities.Count <= 0)
            {

                return BadRequest("Please Enter atlease one ExperienceResponsibilities");
            }
            var saveExperienceResponse = await _experienceRepository.SaveNewExperienceAsync(experience);
            if (!saveExperienceResponse.IsSuccess)
            {
                return BadRequest("Something Went Wrong Please Try Again Later");
            }
            return Ok(saveExperienceResponse);
        }
        #endregion
    }
}
