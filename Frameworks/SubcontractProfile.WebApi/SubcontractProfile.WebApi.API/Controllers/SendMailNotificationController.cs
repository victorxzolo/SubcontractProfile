using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SubcontractProfile.WebApi.Services.Model;

namespace SubcontractProfile.WebApi.API.Controllers
{
    [Route("api/SendMail")]
    [ApiController]
    public class SendMailNotificationController : ControllerBase
    {
        private readonly ILogger<SendMailNotificationController> _logger;

        public SendMailNotificationController(ILogger<SendMailNotificationController> logger)
        {
            _logger = logger;
        }

        [HttpPost("SendMail")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcontractProfileSendMailNotification))]
        public async Task<string> SendMail(SubcontractProfileSendMailNotification emailData)
        {
            _logger.LogInformation($"Start SendMailNotificationController::SendMail", emailData);

            if (emailData == null)
                _logger.LogWarning($"Start SendMailNotificationController::SendMail", emailData);

            try
            {
                var body = "";
                var MailTo = "";
               // var MailCC = "";
                var subject = "";
                //var MailBCC = "";

               
                MailTo = emailData.SendTo;
               // MailCC = emailData.SendCC;
                //MailBCC = emailData.SendBCC;

                string[] MailTo2 = MailTo.Split(';').Where(s => !string.IsNullOrEmpty(s)).ToArray();
                //string[] MailCC2 = MailCC.Split(';').Where(s => !string.IsNullOrEmpty(s)).ToArray();
                //string[] MailBCC2 = MailBCC.Split(';').Where(s => !string.IsNullOrEmpty(s)).ToArray();

                var fromAddress = new MailAddress(emailData.SendFrom);

                var fromPassword = emailData.FromPassword;
                var host = emailData.IPMailServer;
                var port = emailData.Port;
                var domain = emailData.Domaim;

                subject = emailData.Subject;
                body = emailData.Body;

                var smtp = new SmtpClient
                {
                    Host = host,
                    Port = Convert.ToInt32(port),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = true,
                    Credentials = new NetworkCredential(fromAddress.User, fromPassword, domain),
                };

                var message = new MailMessage();
                message.From = fromAddress;

                for (int i = 0; i < MailTo2.Length; i++)
                {
                    message.To.Add(MailTo2[i]);
                }

                //if (MailCC2 != null && MailCC2.Length > 0)
                //{
                //    for (int i = 0; i < MailCC2.Length; i++)
                //    {
                //        message.CC.Add(MailCC2[i]);
                //    }
                //}

                //if (MailBCC2 != null && MailBCC2.Length > 0)
                //{
                //    for (int i = 0; i < MailBCC2.Length; i++)
                //    {
                //        message.Bcc.Add(MailBCC2[i]);
                //    }
                //}
                message.IsBodyHtml = true;
                message.Subject = subject;
                message.Body = body;
                message.Priority = MailPriority.High;

                await smtp.SendMailAsync(message);

                smtp.Dispose();
                message.Dispose();

                emailData.ReturnMessage = "Success";
            }
            catch (Exception ex)
            {
                _logger.LogError($"SendMailNotificationController::SendMail", ex.Message);
                emailData.ReturnMessage = ex.Message;

                throw;
            }

            #region Insert log Send Mail
            #endregion



            return emailData.ReturnMessage;

        }

    }
}
