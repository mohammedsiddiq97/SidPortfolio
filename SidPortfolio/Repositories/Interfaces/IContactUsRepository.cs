using SidPortfolio.DTO;
using SidPortfolio.Infrastructure.DTO;

namespace SidPortfolio.Repositories.Interfaces
{
    public interface IContactUsRepository
    {
        public Task<ResponseModel<string>> SaveNewUserInfoAsync(ContactUsViewModel contactUs);
    }
}
