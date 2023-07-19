using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Domain.Models;
using ViraCMSBackend.Service.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using static ViraCMSBackend.Domain.DTOs.LanguageDTO;
using static ViraCMSBackend.Domain.DTOs.BasicSettingDTO;
using static ViraCMSBackend.Domain.DTOs.BlogDTO;

namespace ViraCMSBackend.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class LanguageController : Controller
    {
        IServiceWrapper _service;
        private readonly AppSettings _appSettings;
        public LanguageController(IServiceWrapper service, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _service = service;
        }
        [AllowAnonymous]
        [HttpGet("GetAllLanguages")]
        public IActionResult GetAllLanguages()
        {
            try
            {
                List<Language> res = _service.Language.GetLanguages();
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "لیست زبان ‌ها با موفقیت ارسال شد", Value = new { response = res }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpGet("BOGetAllLanguages")]
        public IActionResult BOGetAllLanguages()
        {
            try
            {
                List<Language> res = _service.Language.BOGetLanguages();
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "لیست زبان ‌ها با موفقیت ارسال شد", Value = new { response = res }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("AddLanguage")]
        public IActionResult AddLanguage(AddLanguageRequest addLanguageRequest)
        {
            try
            {
                if (addLanguageRequest == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "اطلاعات فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(addLanguageRequest.Title))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "نام زبان فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(addLanguageRequest.Code))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "کد زبان فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(addLanguageRequest.Flag))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "کد زبان فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (addLanguageRequest.IsRTL == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "راستچین یا چپچین بودن زبان فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                Language languageCreated = new Language()
                {
                    Title = addLanguageRequest.Title,
                    Code = addLanguageRequest.Code,
                    Flag = addLanguageRequest.Flag,
                    IsRTL = addLanguageRequest.IsRTL,
                };
                _service.Language.Add(languageCreated);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = " زبان ‌با موفقیت ثبت شد", Value = new { }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("EditLanguage")]
        public IActionResult EditLanguage(EditLanguageRequest editLanguageRequest)
        {
            try
            {
                if (editLanguageRequest == null)
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
                if (editLanguageRequest.Id == null || editLanguageRequest.Id == 0)
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

                Language languageCreated = _service.Language.GetById(editLanguageRequest.Id);
                if (languageCreated == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "زبانی با این شناسه وجود ندارد",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(editLanguageRequest.Title))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "نام زبان فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(editLanguageRequest.Code))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "کد زبان فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(editLanguageRequest.Flag))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "کد زبان فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (editLanguageRequest.IsRTL == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "راستچین یا چپچین بودن زبان فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                languageCreated.Title = editLanguageRequest.Title;
                languageCreated.Code = editLanguageRequest.Code;
                languageCreated.Flag = editLanguageRequest.Flag;
                languageCreated.IsRTL = editLanguageRequest.IsRTL;
                languageCreated.IsActive = editLanguageRequest.IsActive;

                _service.Language.Edit(languageCreated);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = " زبان ‌با موفقیت به روزرسانی شد", Value = new { }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        //[HttpPost("DeleteLanguage")]
        //public IActionResult DeleteLanguage(DeleteLanguage deleteLanguage)
        //{
        //    try
        //    {
        //        if (deleteLanguage.Id == 0 || deleteLanguage.Id == null)
        //        {
        //            return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه زبان نادرستی فرستاده شده است", Value = new { }, Error = new { } });
        //        }
        //        else
        //        {
        //            Language languageCreated = _service.Language.GetById(deleteLanguage.Id);
        //            if (languageCreated == null)
        //            {
        //                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "زبانی با این شناسه وجود ندارد", Value = new { }, Error = new { } });
        //            }
        //            languageCreated.IsDelete = true;
        //            _service.Language.Edit(languageCreated);
        //            return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "زبان با موفقیت حذف شد", Value = new { }, Error = new { } });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
        //    }
        //}
    }
}