using SidPortfolio.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SidPortfolio.Repositories.Interfaces
{
    public interface ICertificationRepository
    {
       public Task<ResponseModel<List<CertificationDto>>> GetCertificatesInfoAsync();
        public Task<ResponseModel<string>> SaveNewCertificateAsync(CertificationViewModel file);
    }
}
