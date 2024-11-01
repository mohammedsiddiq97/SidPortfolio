using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System.Threading.Tasks;

namespace SidPortfolio.Authentication
{
    public class ApiKeyHandler : AuthorizationHandler<ApiKeyRequirement>
    {
        #region Private Variables
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private const string ApiKeyHeaderName = "X-Api-key";
        #endregion

        #region Constructor
        public ApiKeyHandler(IHttpContextAccessor httpContextAccessor,IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }
        #endregion
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, ApiKeyRequirement requirement)
        {

            string apiKey = _httpContextAccessor?.HttpContext?.Request.Headers[ApiKeyHeaderName].ToString();

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                context.Fail();
                return Task.CompletedTask;
            }


            if (!IsApiKeyValid(apiKey))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            context.Succeed(requirement);

            return Task.CompletedTask;
        }
        private bool IsApiKeyValid(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                return false;

            };
            string actualApiKey = _configuration.GetValue<string>("ApiKey");
            if (actualApiKey == null || apiKey != actualApiKey)
                return false;
            return true;

        }
    }
}
