using SidPortfolio.Models;
using System.Threading.Tasks;

namespace SidPortfolio.Infrastructure.Repositories.Services.EmailService
{
    public interface IEmailRepository
    {
        public Task<bool> SendEmail(MessageModel message);
    }
}
