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
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SidPortfolio.Repositories
{
    public class ProjectDetailRepository : IProjectDetailRepository ,IDisposable
    {
        #region Private Variables
        private readonly MyDBContext _myDbContext;
        #endregion
        #region Constructor
        public ProjectDetailRepository(MyDBContext myDbContext)
        {
            _myDbContext = myDbContext;

        }
        #endregion

        public void Dispose()
        {
            _myDbContext.Dispose();
        }

        public async Task<ResponseModel<List<ProjectDetails>>> GetProjectsInfoAsync()
        {
            var projectsDetailList= new ResponseModel<List<ProjectDetails>>();
            try
            {
                projectsDetailList.Value = await (from projectFile in _myDbContext.ProjectFile
                                                    join projectAssociation in _myDbContext.ProjectAssociation
                                                    on projectFile.ProjectId equals projectAssociation.ProjectId
                                                    where projectFile.ActiveStatus == true
                                                    select new ProjectDetails()
                                                    {
                                                        FileName = projectFile.FileName,
                                                        ContentType = projectFile.ContentType,
                                                        Base64Data = Convert.ToBase64String(projectFile.Data),
                                                        ProjectDetailAssociation = new ProjectDetailAssociation()
                                                        {
                                                            Title = projectAssociation.Title,
                                                            Description = projectAssociation.Description,
                                                            GitHubLink = projectAssociation.GitHubLink
                                                        }

                                                    }).ToListAsync();
                projectsDetailList.IsSuccess = true;
                projectsDetailList.StatusCode = 200;
            }
            catch(Exception ex)
            {
                projectsDetailList.Value = new List<ProjectDetails>();
                projectsDetailList.StatusCode = 500;
                projectsDetailList.IsSuccess = false;
            }
          

            return projectsDetailList;
        }

        public async Task<ResponseModel<string>> SaveNewProjectAsync(ProjectViewModel projectDetails)
        {
            var projectDetailResponse = new ResponseModel<string>();

            try
            {
                var permittedExtensions = new[] { ".jpg", ".png", ".jpeg" };
                var extension = Path.GetExtension(projectDetails.File.FileName).ToLowerInvariant();
                if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                {
                    projectDetailResponse.Value="Invalid File Type";
                    projectDetailResponse.IsSuccess = false;
                    projectDetailResponse.StatusCode = 500;
                    return projectDetailResponse;

                }
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(projectDetails.File.FileName);
                var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                var filePath = Path.Combine(uploadDir, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    await projectDetails.File.CopyToAsync(stream);
                }
                var mimeType = projectDetails.File.ContentType;
                var projectFile = new ProjectModel()
                {
                    FileName = uniqueFileName,
                    Size = projectDetails.File.Length,
                    ContentType = mimeType,
                    Data =  await FileHelper.ConvertToByteArray(filePath),
                    ActiveStatus = true,
                    CreationDateTime = DateTime.UtcNow,
                    LastUpdateDateTime = DateTime.UtcNow,
                    ProjectAssociation = new ProjectAssociationModel()
                    {
                        Title = projectDetails.FileTitle,
                        Description = projectDetails.Description,
                        GitHubLink = projectDetails.GitHubLink,
                        ActiveStatus = true,
                        CreationDateTime = DateTime.UtcNow,
                        LastUpdateDateTime = DateTime.UtcNow
                    }

                };
                _myDbContext.ProjectFile.Add(projectFile);
               await _myDbContext.SaveChangesAsync();
                projectDetailResponse.Value = "Uploaded Successfully";
                projectDetailResponse.IsSuccess = true;
                projectDetailResponse.StatusCode = 200;
            }
            catch (Exception e)
            {
                projectDetailResponse.Value = "Project Upload Failed";
                projectDetailResponse.IsSuccess = false;
                projectDetailResponse.StatusCode = 500;

            }
            return projectDetailResponse;
        }

   
    }
}
