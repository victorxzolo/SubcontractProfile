using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SubcontractProfile.WebApi.API.ModelService;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net;
using System.ServiceModel.Security;

namespace SubcontractProfile.WebApi.API.Controllers
{
    [Route("api/AuthenLDAP")]
    [ApiController]
    public class AuthenLDAPController : ControllerBase
    { /// <summary>
      ///  <add key="ProjectCodeLdapFBB" value="FBBWENGAUTH" />
      /// </summary>
       // public string projectCode = "FBBWENGAUTH";
        //[HttpGet("Get/{username}")]
        [HttpGet("GetAuthen/{username}/{password}/{projectCode}")]
        public async Task<HttpResultModel> GetAuthen(string usernname, string password, string projectCode)
        {
            HttpResultModel httpResult = new HttpResultModel();
           
            try {
                if (!string.IsNullOrWhiteSpace(usernname) && !string.IsNullOrWhiteSpace(password) && !string.IsNullOrWhiteSpace(projectCode))
                {
                    AuthenLDAP.CorporateSoapClient service = new AuthenLDAP.CorporateSoapClient();
                    service.ClientCredentials.ServiceCertificate.SslCertificateAuthentication =
                   new X509ServiceCertificateAuthentication()
                   {
                       CertificateValidationMode = X509CertificateValidationMode.None,
                       RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck
                   };
                    ChannelFactory<AuthenLDAP.CorporateSoap> channelFactory = service.ChannelFactory;
                    AuthenLDAP.CorporateSoap channel = channelFactory.CreateChannel();
                    AuthenLDAP.WS_GEN_AuthenLDAPRequest serviceRequest = new AuthenLDAP.WS_GEN_AuthenLDAPRequest
                    {
                        Body = new AuthenLDAP.WS_GEN_AuthenLDAPRequestBody
                        {
                            userName = usernname,
                            passWd = password,
                            projectCode = projectCode,
                        }
                    };
                    AuthenLDAP.WS_GEN_AuthenLDAPResponse responseMessage = await channel.WS_GEN_AuthenLDAPAsync(serviceRequest);
                   var ddd  = responseMessage?.Body.WS_GEN_AuthenLDAPResult;
                    httpResult.SetPropertyHttpResult(httpResult, true, "", "", StatusCodes.Status200OK, ddd);
                   // return httpResult;
                }
                else
                {
                    httpResult.SetPropertyHttpResult(httpResult, true, "", "", StatusCodes.Status400BadRequest);
                    //  return httpResult;
                }
                return httpResult;

            }
            catch (Exception ex)
            {
               
                httpResult.SetPropertyHttpResult(httpResult, false, "", ex.Message, StatusCodes.Status500InternalServerError);
                return httpResult;
            }
        }
    }
}