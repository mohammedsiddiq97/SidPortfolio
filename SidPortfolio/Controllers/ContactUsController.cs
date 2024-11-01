using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SidPortfolio.DTO;
using SidPortfolio.Infrastructure.DTO;
using SidPortfolio.Models;
using SidPortfolio.Repositories.Interfaces;
using System.Threading.Tasks;

namespace SidPortfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "ApiKeyPolicy")]

    public class ContactUsController : ControllerBase
    {
        #region Private Variables
        private readonly IContactUsRepository _contactusRepository;
        #endregion

        #region Constructor
        public ContactUsController(IContactUsRepository contactusRepository)
        {
            _contactusRepository = contactusRepository;
        }
        #endregion

        #region Public Methods

        [HttpPost("SaveNewUser")]
        [ProducesResponseType(typeof(ResponseModel<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SaveNewUserInfoAsync([FromBody]ContactUsViewModel contactUs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Input values");
            }
           
            var userDetailsResponse = await _contactusRepository.SaveNewUserInfoAsync(contactUs).ConfigureAwait(false);
            if (!userDetailsResponse.IsSuccess)
            {
                return BadRequest("Something Went Wrong While Saving the NewUserInfo, Please Try Again Later");
            }
            return Ok(userDetailsResponse);
                
            
            
        }
    }
    #endregion
}
