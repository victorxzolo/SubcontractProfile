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

namespace SubcontractProfile.WebApi.API.Controllers
{
    [Route("api/VATService")]
    [ApiController]
    public class VATServiceController : ControllerBase
    {
        public const string soapUsername = "anonymous";
        public const string soapPassword = "anonymous";

        [HttpGet("Get/{tIN}")]
        public async Task<HttpResultModel> GetById(string tIN)
        {
            HttpResultModel httpResult = new HttpResultModel();
            try
            {
                if (!string.IsNullOrWhiteSpace(tIN))
                {
                    VATSoapService.vatserviceRD3SoapClient service = new VATSoapService.vatserviceRD3SoapClient();
                    ChannelFactory<VATSoapService.vatserviceRD3Soap> channelFactory = service.ChannelFactory;
                    // Must set certificates before CreateChannel()
                    //string certificatepath1= Path.GetFullPath(Path.Combine("Resources", "adhq1.cer"));
                    //string certificatepath2= Path.GetFullPath(Path.Combine("Resources", "ADHQ5.cer"));
                    ////string certificatepath1 = @"C:\Users\Me\source\repos\RevenueService\RevenueService.Api\Resources\adhq1.cer";
                    ////string certificatepath2 = @"C:\Users\Me\source\repos\RevenueService\RevenueService.Api\Resources\ADHQ5.cer";
                    //X509Certificate2 certificate1 = new X509Certificate2(System.IO.File.ReadAllBytes(certificatepath1));
                    //X509Certificate2 certificate2 = new X509Certificate2(System.IO.File.ReadAllBytes(certificatepath2));
                    //channelFactory.Credentials.ClientCertificate.Certificate = certificate1;
                    //channelFactory.Credentials.ServiceCertificate.DefaultCertificate = certificate2;

                   

                    VATSoapService.vatserviceRD3Soap channel = channelFactory.CreateChannel();
                    VATSoapService.ServiceRequest serviceRequest = new VATSoapService.ServiceRequest
                    {
                        Body = new VATSoapService.ServiceRequestBody
                        {
                            username = soapUsername,
                            password = soapPassword,
                            TIN = tIN,
                            Name = "",
                            ProvinceCode = 0,
                            BranchNumber = 0,
                            AmphurCode = 0,
                        }
                    };
                    ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                    VATSoapService.ServiceResponse responseMessage = await channel.ServiceAsync(serviceRequest);
                    VATSoapService.vat soapResult = responseMessage?.Body.ServiceResult;
                    VAT vat = new VAT(soapResult);
                    httpResult.SetPropertyHttpResult(httpResult, true, "", "", StatusCodes.Status200OK, vat);
                }
                else
                {
                    httpResult.SetPropertyHttpResult(httpResult, true, "", "", StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception ex)
            {
                httpResult.SetPropertyHttpResult(httpResult, false, "", ex.Message, StatusCodes.Status500InternalServerError);
            }

            return httpResult;
        }

        [HttpGet("Get")]
        public async Task<HttpResultModel> Get([FromBody] VAT model)
        {
            HttpResultModel httpResult = new HttpResultModel();
            try
            {
                VATSoapService.vatserviceRD3SoapClient service = new VATSoapService.vatserviceRD3SoapClient();
                ChannelFactory<VATSoapService.vatserviceRD3Soap> channelFactory = service.ChannelFactory;
                VATSoapService.vatserviceRD3Soap channel = channelFactory.CreateChannel();
                VATSoapService.ServiceRequest serviceRequest = new VATSoapService.ServiceRequest
                {
                    Body = new VATSoapService.ServiceRequestBody
                    {
                        username = soapUsername,
                        password = soapPassword,
                        TIN = model.vtin,
                        Name = model.vName,
                        ProvinceCode = Convert.ToInt32(model.vProvince),
                        BranchNumber = Convert.ToInt32(model.vBranchNumber),
                        AmphurCode = Convert.ToInt32(model.vAmphur),
                    }
                };

                VATSoapService.ServiceResponse responseMessage = await channel.ServiceAsync(serviceRequest);
                VATSoapService.vat soapResult = responseMessage?.Body.ServiceResult;
                VAT vatModel = new VAT(soapResult);

                httpResult.SetPropertyHttpResult(httpResult, true, "", "", StatusCodes.Status200OK, vatModel);
            }
            catch (Exception ex)
            {
                httpResult.SetPropertyHttpResult(httpResult, false, "API Error", ex.Message, StatusCodes.Status500InternalServerError);
            }

            return httpResult;
        }
    }
}
