using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using ViraCMSBackend.Core.Helpers;
using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Domain.Models;
using ViraCMSBackend.Service.Base;
using static ViraCMSBackend.Domain.DTOs.BlogDTO;
using static ViraCMSBackend.Domain.DTOs.DashboardDTO;

namespace ViraCMSBackend.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class DashboardController : Controller
    {
        IServiceWrapper _service;
        private readonly AppSettings _appSettings;
        public DashboardController(IServiceWrapper service, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _service = service;
        }
        [HttpGet("BOGetMostViwedBlogs")]
        public IActionResult BOGetMostViwedBlogs([FromHeader] string? language)
        {
            try
            {
                var languageId = 0;
                if (!string.IsNullOrEmpty(language))
                {
                    if (!TextHelpers.IsDigitsOnly(language))
                    {
                        Language _language = _service.Language.GetByCode(language);
                        if (_language != null)
                        {
                            languageId = _language.Id;
                        }
                    }
                    if (TextHelpers.IsDigitsOnly(language))
                    {
                        languageId = Convert.ToInt32(language);
                    }
                }
                List<MostViewedBlogs> res = _service.Dashboard.GetMostViwedBlogs(languageId);
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "بلاگ های پر بازدید با موفقیت ارسال شد",
                    Value = new { response = res },
                    Error = new { }
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.InternalServerError,
                    Message = "خطای داخلی سرور رخ داده است",
                    Value = new { },
                    Error = new { Response = ex.ToString() }
                });
            }
        }
        [HttpGet("BOGetMostViwedNews")]
        public IActionResult BOGetMostViwedNews([FromHeader] string? language)
        {
            try
            {
                var languageId = 0;
                if (!string.IsNullOrEmpty(language))
                {
                    if (!TextHelpers.IsDigitsOnly(language))
                    {
                        Language _language = _service.Language.GetByCode(language);
                        if (_language != null)
                        {
                            languageId = _language.Id;
                        }
                    }
                    if (TextHelpers.IsDigitsOnly(language))
                    {
                        languageId = Convert.ToInt32(language);
                    }
                }
                List<MostViewedNews> res = _service.Dashboard.GetMostViwedNews(languageId);
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "اخبار پر بازدید با موفقیت ارسال شد",
                    Value = new { response = res },
                    Error = new { }
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.InternalServerError,
                    Message = "خطای داخلی سرور رخ داده است",
                    Value = new { },
                    Error = new { Response = ex.ToString() }
                });
            }
        }
        [HttpGet("BOGetMostViwedPages")]
        public IActionResult BOGetMostViwedPages([FromHeader] string? language)
        {
            try
            {
                var languageId = 0;
                if (!string.IsNullOrEmpty(language))
                {
                    if (!TextHelpers.IsDigitsOnly(language))
                    {
                        Language _language = _service.Language.GetByCode(language);
                        if (_language != null)
                        {
                            languageId = _language.Id;
                        }
                    }
                    if (TextHelpers.IsDigitsOnly(language))
                    {
                        languageId = Convert.ToInt32(language);
                    }
                }
                List<MostViewedPages> res = _service.Dashboard.GetMostViwedPages(languageId);
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "صفحات پر بازدید با موفقیت ارسال شد",
                    Value = new { response = res },
                    Error = new { }
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.InternalServerError,
                    Message = "خطای داخلی سرور رخ داده است",
                    Value = new { },
                    Error = new { Response = ex.ToString() }
                });
            }
        }
        [HttpGet("BOGetMostCountryVisits")]
        public IActionResult BOGetMostCountryVisits()
        {
            try
            {
                List<MostCountryVisits> res = _service.Dashboard.GetMostCountryVisits();
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "کشور ها بر اساس بیشترین بازدید با موفقیت ارسال شد",
                    Value = new { response = res },
                    Error = new { }
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.InternalServerError,
                    Message = "خطای داخلی سرور رخ داده است",
                    Value = new { },
                    Error = new { Response = ex.ToString() }
                });
            }
        }
    }
}
