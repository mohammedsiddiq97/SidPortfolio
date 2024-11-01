using SidPortfolio.DTO;
using SidPortfolio.Infrastructure.DTO;
using SidPortfolio.Infrastructure.Repositories.Services.EmailService;
using SidPortfolio.Repositories.Interfaces;
using SidPortfolio.Models;
using System.Threading.Tasks;
using System;
using SidPortfolio.DBContext;
using System.Collections.Generic;

namespace SidPortfolio.Repositories
{
    public class ContactUsRepository : IContactUsRepository ,IDisposable
    {
        #region private Variables
        private readonly MyDBContext _myDbContext;
        private readonly IEmailRepository _emailRepository;
        private readonly EmailConfigurationModel _emailConfig;
        #endregion

        #region Constructor
        public ContactUsRepository(MyDBContext myDBContext, IEmailRepository emailRepository,
            EmailConfigurationModel emailConfig)
        {
            _myDbContext = myDBContext;
            _emailRepository = emailRepository;
            _emailConfig = emailConfig;
        }
        #endregion

        public void Dispose()
        {
            _myDbContext.Dispose();
        }

        public async Task<ResponseModel<string>> SaveNewUserInfoAsync(ContactUsViewModel contactDetails)
        {
            var  contactUsResponse = new ResponseModel<string>();
            try
            {
                var userDetails = new ContactUsModel()
                {
                    Name = contactDetails.Name,
                    PhoneNumber = contactDetails.PhoneNumber,
                    Email = contactDetails.Email,
                    Description = contactDetails.Description,
                    ActiveStatus = true,
                    CreationDateTime = DateTime.UtcNow,
                    LastUpdateDateTime = DateTime.UtcNow
                };
                _myDbContext.ContactUs.Add(userDetails);
                await _myDbContext.SaveChangesAsync();
                var emailBody = $@"
                                <!DOCTYPE html>
                               <html>
                                <head>
                                    <style>
                                        body {{
                                            font-family: Arial, sans-serif;
                                            background-color: #f4f4f4;
                                            padding: 20px;
                                        }}
                                        .container {{
                                            background-color: #ffffff;
                                            padding: 20px;
                                            border-radius: 5px;
                                            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                                        }}
                                        h2 {{
                                            color: #333333;
                                        }}
                                        p {{
                                            color: #666666;
                                            line-height: 1.5;
                                        }}
                                        .label {{
                                            font-weight: bold;
                                        }}
                                    </style>
                                </head>
                                <body>
                                    <div class='container'>
                                        <h2>Hi Mohammed Abdul Siddiq,</h2>
                                        <p>There is one message for you:</p>
                                        <p><span class='label'>Name:</span> {userDetails.Name}</p>
                                        <p><span class='label'>Mobile Number:</span> {userDetails.PhoneNumber}</p>
                                        <p><span class='label'>Email:</span> {userDetails.Email}</p>
                                        <p><span class='label'>Description:</span> {userDetails.Description}</p>
                                    </div>
                                </body>
                                </html>
                                ";


                var sendEmail = new MessageModel(new List<string> { _emailConfig.To }, "Mail From the SidPortfolio", emailBody);
                var emailResponse = await _emailRepository.SendEmail(sendEmail);
                contactUsResponse.IsSuccess = emailResponse;
                contactUsResponse.Value = emailResponse ? "Email has been sent" : "Failed to sent the email";
                contactUsResponse.StatusCode = 200;

            }
            catch (Exception ex) {
                contactUsResponse.IsSuccess = false;
                contactUsResponse.StatusCode = 500;
                contactUsResponse.Value = "Exception While sending Email";
            }
            return contactUsResponse;
        }
    }
}
