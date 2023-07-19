using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using ViraCMSBackend.Core.Helpers;
using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Domain.Models;
using ViraCMSBackend.Service.Base;
using static ViraCMSBackend.Domain.DTOs.BlogDTO;
using static ViraCMSBackend.Domain.DTOs.PageDTO;

namespace ViraCMSBackend.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class PageController : Controller
    {
        IServiceWrapper _service;
        private readonly AppSettings _appSettings;
        public PageController(IServiceWrapper service, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _service = service;
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody] AddPageRequest addPageRequest)
        {
            try
            {
                if (addPageRequest == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "اطلاعات فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (addPageRequest.LanguageId == null || addPageRequest.LanguageId == 0)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "شناسه زبان فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(addPageRequest.Name))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "نام صفحه فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (_service.Page.GetByAlias(addPageRequest.Name.Trim().Replace("  ", " ").Replace(" ", "-").Replace("\\", "-").Replace("/", "-").Replace("+", "")) != null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "نام صفحه تکراری است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(addPageRequest.CustomHTML))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "جزئیات صفحه فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                Page PageCreated = new Page()
                {
                    LanguageId = addPageRequest.LanguageId,
                    Name = addPageRequest.Name,
                    Alias = addPageRequest.Name.Trim().Replace("  ", " ").Replace(" ", "-").Replace("\\", "-").Replace("/", "-").Replace("+", ""),
                    CustomHTML = addPageRequest.CustomHTML.Trim(),
                };
                _service.Page.Add(PageCreated);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "صفحه با موفقیت ثبت شد", Value = new { }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("Edit")]
        public IActionResult Edit([FromBody] EditPageRequest editPageRequest)
        {
            try
            {
                if (editPageRequest == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "اطلاعات فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (editPageRequest.Id == null || editPageRequest.Id == 0)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "شناسه صفحه فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }

                Page PageCreated = _service.Page.GetById(editPageRequest.Id);
                if (PageCreated == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "صفحهی با این شناسه وجود ندارد",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (editPageRequest.LanguageId == null || editPageRequest.LanguageId == 0)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "شناسه زبان فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(editPageRequest.Name))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "نام صفحه فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                var x = _service.Page.GetByAlias(editPageRequest.Name.Trim().Replace("  ", " ").Replace(" ", "-").Replace("\\", "-").Replace("/", "-").Replace("+", ""));
                if (x != null && x.Id != editPageRequest.Id)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "نام صفحه تکراری است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(editPageRequest.CustomHTML))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "جزئیات صفحه فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                PageCreated.LanguageId = editPageRequest.LanguageId.GetValueOrDefault();
                PageCreated.Name = editPageRequest.Name.Trim();
                PageCreated.Alias = editPageRequest.Name.Trim().Replace("  ", " ").Replace(" ", "-").Replace("\\", "-").Replace("/", "-").Replace("+", "");
                PageCreated.CustomHTML = editPageRequest.CustomHTML.Trim();
                PageCreated.IsActive = editPageRequest.IsActive;
                _service.Page.Edit(PageCreated);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "صفحه با موفقیت به روز رسانی شد", Value = new { }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] DeletePage deletePage)
        {
            try
            {
                if (deletePage.Id == 0 || deletePage.Id == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه صفحه نادرستی فرستاده شده است", Value = new { }, Error = new { } });
                }
                else
                {
                    Page PageCreated = _service.Page.GetById(deletePage.Id);
                    if (PageCreated == null)
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "صفحهی با این شناسه وجود ندارد", Value = new { }, Error = new { } });
                    }
                    PageCreated.IsDelete = true;
                    _service.Page.Edit(PageCreated);
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "صفحه با موفقیت حذف شد", Value = new { }, Error = new { } });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [AllowAnonymous]
        [HttpGet("GetAll")]
        public IActionResult GetAll([FromHeader] string? language)
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
                List<ShowPages> res = _service.Page.GetAll(languageId);
                List<KeyValuePair<string, List<ShowPages>>> keyValuePair = new List<KeyValuePair<string, List<ShowPages>>>();
                var languages = _service.Language.GetLanguages();
                foreach (var item in languages)
                {
                    keyValuePair.Add(new KeyValuePair<string, List<ShowPages>>(item.Code, res.Where(w => w.LanguageId == item.Id).ToList()));
                }
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "صفحه ها با موفقیت ارسال شد", Value = new { response = keyValuePair.ToDictionary(kvp => kvp.Key, kvp => kvp.Value) }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpGet("BOGetAll")]
        public IActionResult BOGetAll([FromHeader] string? language)
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
                List<BOShowPages> res = _service.Page.BOGetAll(languageId);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "صفحه ها با موفقیت ارسال شد", Value = new { response = res }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("GetById")]
        public IActionResult GetById([FromHeader] int id)
        {
            try
            {
                Page res = _service.Page.GetById(id);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "صفحه با موفقیت ارسال شد", Value = new { response = res }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [AllowAnonymous]
        [HttpPost("GetByAlias")]
        public IActionResult GetByAlias([FromBody] GetPageByAlias Alias)
        {
            try
            {
                Page res = _service.Page.GetByAlias(Alias.Alias);
                if (res == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "صفحهی با این شناسه وجود ندارد",
                        Value = new { },
                        Error = new { }
                    });
                }
                res.ViewCounter++;
                _service.Page.Edit(res);
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "صحفه با موفقیت ارسال شد",
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
