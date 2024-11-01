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
    public class CertificationController : ControllerBase
    {
        #region Private Variables
        private readonly ICertificationRepository _certificationRepository;
        #endregion

        #region Constructor
        public CertificationController(ICertificationRepository certificationRepository)
        {
            _certificationRepository = certificationRepository;   
        }
        #endregion

        #region Public Methods

        [HttpGet]
        [ProducesResponseType(typeof(ResponseModel<List<CertificationDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCertificatesInfoAsync()
        {
            var certificationResponse = await _certificationRepository.GetCertificatesInfoAsync();
            if (!certificationResponse.IsSuccess)
            {
                return BadRequest("Something Went Wrong While Fetching the CertificateInfo, Please Try Again Later");
            }
            return Ok(certificationResponse);
        }
        [HttpPost("SaveNewCertification")]
        [ProducesResponseType(typeof(ResponseModel<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SaveNewCertificateAsync(CertificationViewModel certificationView)
        {
            if (certificationView.File == null || certificationView.File.Length == 0)
            {
               
                return BadRequest("Please Upload Proper Image");
            }
            var saveCertificationResponse = await _certificationRepository.SaveNewCertificateAsync(certificationView);
            if (!saveCertificationResponse.IsSuccess)
            {
                return BadRequest("Something Went Wrong While Saving the CertificateInfo, Please Try Again Later");
            }
            return Ok(saveCertificationResponse);
        }
        #endregion
    }
}
