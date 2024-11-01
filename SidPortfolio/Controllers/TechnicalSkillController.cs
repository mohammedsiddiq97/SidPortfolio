using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SidPortfolio.DTO;
using SidPortfolio.Repositories;
using SidPortfolio.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SidPortfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "ApiKeyPolicy")]
    public class TechnicalSkillController : ControllerBase
    {
        #region Private Variables
        private readonly ITechnicalSkillRepository _technicalSkillRepository;
        #endregion

        #region Constructor
        public TechnicalSkillController(ITechnicalSkillRepository technicalSkillRepository)
        {
            _technicalSkillRepository = technicalSkillRepository;
                
        }
        #endregion

        #region Public Methods
        [HttpGet]
        [ProducesResponseType(typeof(ResponseModel<List<string>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTechnicalSkillsInfoAsync()
        {
            var technicalSkillResponse = await _technicalSkillRepository.GetTechnicalSkillsInfoAsync();
            if (!technicalSkillResponse.IsSuccess)
            {
                return BadRequest("Something Went Wrong While fetching the TechnicalSkillsInfo, Please Try Again Later");
            }
            return Ok(technicalSkillResponse);
        }
        [HttpPost("SaveNewTechnicalSkills")]
        [ProducesResponseType(typeof(ResponseModel<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SaveNewTechnicalSkillAsync(string skill)
        {
            if (string.IsNullOrEmpty(skill))
            {

                return BadRequest("Please provide atleast one Skill");
            }
            var saveTechnicalSkillResponse = await _technicalSkillRepository.SaveNewTechnicalSkillAsync(skill);
            if (!saveTechnicalSkillResponse.IsSuccess)
            {
                return BadRequest("Something Went Wrong While Saving the TechnicalSkillInfo, Please Try Again Later");
            }
            return Ok(saveTechnicalSkillResponse);
        }
        #endregion
    }
}
