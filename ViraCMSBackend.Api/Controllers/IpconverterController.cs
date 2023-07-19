using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Service.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using ViraCMSBackend.Domain.Models;

namespace ViraCMSBackend.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IpconverterController : Controller
    {
        IServiceWrapper _service;
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;
        public IpconverterController(IServiceWrapper service, IOptions<AppSettings> appSettings, IRepositoryWrapper repository)
        {
            _appSettings = appSettings.Value;
            _service = service;
            _repository = repository;
        }
        [HttpPost("GetCountryCode")]
        public IActionResult GetCountryCode([FromHeader] string ip)
        {
            try
            {
                var res = _service.IpconverterService.IpToCountryCode(ip);
                if(res == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "کد کشور با موفقیت ارسال شد", Value = new { response = _appSettings.DefaultLanguage }, Error = new { } }); ;
                }
                var code = _repository.IpConverter.CountryNameToCountryCode(res.Country,_appSettings.DefaultLanguage);

                Visit visitCreate = new Visit();
                if (res.Status == "ERROR")
                {
                    var x = _service.Language.GetByCode(_appSettings.DefaultLanguage);
                    visitCreate.Country = x.Title;
                    visitCreate.IP = "Invalid IP Address";
                    visitCreate.Code = x.Code;
                    visitCreate.LanguageCode = x.Code;
                }
                else
                {
                    visitCreate.Country = res.Country;
                    visitCreate.IP = res.IP;
                    visitCreate.Code = res.Code;
                    visitCreate.LanguageCode = code;
                }
                _service.Visit.Add(visitCreate);
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "کد کشور با موفقیت ارسال شد", Value = new { response = code }, Error = new { } }); ;
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
    }
}
