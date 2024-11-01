using Microsoft.EntityFrameworkCore;
using SidPortfolio.DBContext;
using SidPortfolio.DTO;
using SidPortfolio.Helper;
using SidPortfolio.Models;
using SidPortfolio.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace SidPortfolio.Repositories
{
    public class CertificationRepository : ICertificationRepository , IDisposable
    {
        private readonly MyDBContext _myDbContext;
        public CertificationRepository(MyDBContext myDBContext)
        {
            _myDbContext = myDBContext;
                
        }

        public void Dispose()
        {
            _myDbContext.Dispose();
        }

        public async Task<ResponseModel<List<CertificationDto>>> GetCertificatesInfoAsync()
        {
            var certificationsList = new ResponseModel<List<CertificationDto>>();

            try
            {
                certificationsList.Value = await (from certification in _myDbContext.Certifications
                                                    join certificationAssociation in _myDbContext.CertificationAssociation
                                                    on certification.CertificationId equals certificationAssociation.CertificationId
                                                    where certification.ActiveStatus == true
                                                    select new CertificationDto()
                                                    {
                                                        FileName = certification.FileName,
                                                        ContentType = certification.ContentType,
                                                        Base64Data = Convert.ToBase64String(certification.Data),
                                                        CertificationAssociation = new CertificationAssociationDto()
                                                        {
                                                            Title = certificationAssociation.Title,
                                                            IssuingOrganization = certificationAssociation.IssuingOrganization,
                                                            Description = certificationAssociation.Description
                                                        }

                                                    }).ToListAsync();
                certificationsList.IsSuccess = true;
                certificationsList.StatusCode = 200;

            }
            catch (Exception ex)
            {
                certificationsList.StatusCode = 500;
                certificationsList.IsSuccess = false;

            }


            return certificationsList;




        }

        public async Task<ResponseModel<string>> SaveNewCertificateAsync(CertificationViewModel certificationView)
        {
            var certificationDetailResponse = new ResponseModel<string>();

            try
            {
                var permittedExtensions = new[] { ".jpg", ".png", ".jpeg" };
                var extension = Path.GetExtension(certificationView.File.FileName).ToLowerInvariant();
                if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                {
                    certificationDetailResponse.Value = "Failed to Upload";
                    certificationDetailResponse.StatusCode = 500;
                    certificationDetailResponse.IsSuccess = false;
                    return certificationDetailResponse;
                }
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(certificationView.File.FileName);
                var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                var filePath = Path.Combine(uploadDir,uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    await certificationView.File.CopyToAsync(stream);
                }
                var mimeType = certificationView.File.ContentType;
                var certificationFile = new CertificationModel
                {
                    FileName = uniqueFileName,
                    Size = certificationView.File.Length,
                    ContentType = mimeType,
                    Data = await FileHelper.ConvertToByteArray(filePath),
                    ActiveStatus = true,
                    CreationDateTime = DateTime.UtcNow,
                    LastUpdateDateTime = DateTime.UtcNow,
                    CertificationAssociation = new CertificationAssociationModel()
                    {
                        Title = certificationView.FileTitle,
                        IssuingOrganization = certificationView.IssuingOrganization,
                        Description = certificationView.Description,
                        ActiveStatus = true,
                        CreationDateTime = DateTime.UtcNow,
                        LastUpdateDateTime = DateTime.UtcNow
                    }

                };
                _myDbContext.Certifications.Add(certificationFile);
                  await _myDbContext.SaveChangesAsync();
                 certificationDetailResponse.Value = "UploadSuccess";
                certificationDetailResponse.StatusCode = 200;
                certificationDetailResponse.IsSuccess = true;
               
            }
            catch(Exception e)
            {
                certificationDetailResponse.Value = "Failed to Upload";
                certificationDetailResponse.StatusCode = 500;
                certificationDetailResponse.IsSuccess = false;
            }
            return certificationDetailResponse;
        }

    }


 
}
