using Microsoft.EntityFrameworkCore;
using SidPortfolio.DBContext;
using SidPortfolio.DTO;
using SidPortfolio.Models;
using SidPortfolio.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SidPortfolio.Repositories
{
    public class ExperienceRepository : IExperienceRepository, IDisposable
    {
        #region Constructor
        private readonly MyDBContext _myDbContext;
        #endregion
        #region Constrcutor
        public ExperienceRepository(MyDBContext myDBContext)
        {
            _myDbContext = myDBContext;

        }

        public void Dispose()
        {
            _myDbContext.Dispose();
        }
        #endregion
        public async Task<ResponseModel<List<ExperienceDto>>> GetExperienceInfoAsync()
        {
            var experiencesList = new ResponseModel<List<ExperienceDto>>();

            try
            {
                experiencesList.Value = await (from experience in _myDbContext.Experience
                                               where experience.ActiveStatus == true
                                               select new ExperienceDto()
                                               {
                                                   JobTitle = experience.JobTitle,
                                                   Company = experience.Company,
                                                   Duration = experience.Duration,
                                                   ExperienceAssociations = (from experienceResponsibilities in _myDbContext.ExperienceResponsibilitiesAssociation
                                                                             where experienceResponsibilities.ExperienceId == experience.ExperienceId
                                                                             select new ExperienceResponsibilitiesAssociationDto()
                                                                             {
                                                                                 ExperienceId = experienceResponsibilities.ExperienceId,
                                                                                 Responsibilities = experienceResponsibilities.Responsibilities
                                                                             }).ToList()

                                               }).ToListAsync();

                experiencesList.IsSuccess = true;
                experiencesList.StatusCode = 200;

            }
            catch (Exception ex)
            {
                experiencesList.StatusCode = 500;
                experiencesList.IsSuccess = false;

            }


            return experiencesList;
        }

        public async Task<ResponseModel<string>> SaveNewExperienceAsync(ExperienceViewModel experience)
        {
            var experienceResponse = new ResponseModel<string>();
            try
            {
                var experienceModel = new ExperienceModel()
                {
                    JobTitle = experience.JobTitle,
                    Company = experience.Company,
                    Duration = experience.Duration,
                    ActiveStatus = true,
                    CreationDateTime = DateTime.UtcNow,
                    LastUpdateDateTime = DateTime.UtcNow,
                    ExperienceResponsibilitiesAssociation = experience.Responsibilities.Select(exp => new ExperienceResponsibilitiesAssociationModel
                    {
                        Responsibilities = exp,
                        ActiveStatus = true,
                        CreationDateTime = DateTime.UtcNow,
                        LastUpdateDateTime = DateTime.UtcNow
                    }).ToList()

                };
                _myDbContext.Experience.Add(experienceModel);
                await _myDbContext.SaveChangesAsync();
                experienceResponse.Value = "Added Successfully";
                experienceResponse.IsSuccess = true;
                experienceResponse.StatusCode = 200;
            }
            catch (Exception ex)
            {
                experienceResponse.Value = "Failed";
                experienceResponse.IsSuccess = true;
                experienceResponse.StatusCode = 200;
            }


            return experienceResponse;
        }

    }
}
