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
    public class TechnicalSkillRepository : ITechnicalSkillRepository , IDisposable
    {
        #region Private Variables
        private readonly MyDBContext _myDbContext;
        #endregion

        #region Constructor
        public TechnicalSkillRepository(MyDBContext myDBContext)
        {
            _myDbContext = myDBContext;
        }
        #endregion

        public void Dispose()
        {
            _myDbContext.Dispose();
        }

        public async Task<ResponseModel<List<string>>> GetTechnicalSkillsInfoAsync()
        {
            var technicalSkillsList = new ResponseModel<List<string>>();

            try
            {
                technicalSkillsList.Value = await _myDbContext.TechnicalSkill.Where(i => i.ActiveStatus).Select(x => x.SkillName).ToListAsync();
                technicalSkillsList.IsSuccess = true;
                technicalSkillsList.StatusCode = 200;

            }
            catch (Exception ex)
            {
                technicalSkillsList.Value = new List<string>();
                technicalSkillsList.IsSuccess = true;
                technicalSkillsList.StatusCode = 200;
            }
            return technicalSkillsList;
        }

        public async Task<ResponseModel<string>> SaveNewTechnicalSkillAsync(string skillName)
        {
            var technicalSkillResponse = new ResponseModel<string>();
            try
            {
                var technicalSkill = new TechnicalSkillModel()
                {
                    SkillName = skillName,
                    ActiveStatus = true,
                    CreationDateTime = DateTime.UtcNow,
                    LastUpdateDateTime = DateTime.UtcNow,
                };
                _myDbContext.TechnicalSkill.Add(technicalSkill);
                  await _myDbContext.SaveChangesAsync();
                technicalSkillResponse.Value = "Added Successfully";
                technicalSkillResponse.IsSuccess = true;
                technicalSkillResponse.StatusCode = 200;
            }
            catch (Exception ex)
            {
                technicalSkillResponse.Value = "Failed";
                technicalSkillResponse.IsSuccess = true;
                technicalSkillResponse.StatusCode = 200;
            }


            return technicalSkillResponse;
        }
    }
}
