using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using ViraCMSBackend.Core.Helpers;
using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Domain.Models;
using ViraCMSBackend.Service.Base;
using static ViraCMSBackend.Domain.DTOs.CounterDTO;

namespace ViraCMSBackend.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class CounterController : Controller
    {
        IServiceWrapper _service;
        private readonly AppSettings _appSettings;
        public CounterController(IServiceWrapper service, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _service = service;
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody] AddCounterRequest addCounterRequest)
        {
            try
            {
                if (addCounterRequest == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "اطلاعات فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (addCounterRequest.LanguageId == null || addCounterRequest.LanguageId == 0)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه زبان فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(addCounterRequest.Title))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "عنوان شمارنده فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(addCounterRequest.Number))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "تعداد(مقدار) شمارنده فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(addCounterRequest.Icon))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "ایکون شمارنده فرستاده نشده است", Value = new { }, Error = new { } });
                }
                var icon = _service.Picture.Upload(DateTime.Now.ToString("MMddHHmmss") + "-" + addCounterRequest.IconName, addCounterRequest.Icon, false, 5);
                Counter CounterCreated = new Counter()
                {
                    LanguageId = addCounterRequest.LanguageId,
                    Title = addCounterRequest.Title.Trim().Replace("  ", " "),
                    Number = addCounterRequest.Number,
                    Icon = icon.Address,
                };
                _service.Counter.Add(CounterCreated);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "شمارنده با موفقیت ثبت شد", Value = new { }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("Edit")]
        public IActionResult Edit([FromBody] EditCounterRequest editCounterRequest)
        {
            try
            {
                if (editCounterRequest == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "اطلاعات فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (editCounterRequest.Id == null || editCounterRequest.Id == 0)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه شمارنده فرستاده نشده است", Value = new { }, Error = new { } });
                }

                Counter CounterCreated = _service.Counter.GetById(editCounterRequest.Id);
                if (CounterCreated == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شمارندای با این شناسه وجود ندارد", Value = new { }, Error = new { } });
                }
                if (editCounterRequest.LanguageId == null || editCounterRequest.LanguageId == 0)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه زبان فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(editCounterRequest.Title))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "عنوان شمارنده فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(editCounterRequest.Number))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "تعداد(مقدار) شمارنده فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(editCounterRequest.Icon))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "آیکون شمارنده فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (editCounterRequest.Icon == CounterCreated.Icon)
                {
                    CounterCreated.Icon = CounterCreated.Icon;
                }
                if (editCounterRequest.Icon != CounterCreated.Icon)
                {
                    var res = _service.Picture.GetByAddress(CounterCreated.Icon);
                    if (res != null)
                    {
                        var imageName = res.Address.Split("/")[res.Address.Split("/").Count() - 1];
                        if (System.IO.File.Exists(_appSettings.SaveImagePath + "\\Counters\\" + imageName))
                            System.IO.File.Delete(_appSettings.SaveImagePath + "\\Counters\\" + imageName);
                        _service.Picture.DeleteById(res.Id);
                    }
                    var icon = _service.Picture.Upload(DateTime.Now.ToString("MMddHHmmss") + "-" + editCounterRequest.IconName, editCounterRequest.Icon, false, 5);
                    CounterCreated.Icon = icon.Address;
                }
                CounterCreated.LanguageId = editCounterRequest.LanguageId.GetValueOrDefault();
                CounterCreated.Title = editCounterRequest.Title.Trim().Replace("  ", " ");
                CounterCreated.Number = editCounterRequest.Number;
                CounterCreated.IsActive = editCounterRequest.IsActive;
                _service.Counter.Edit(CounterCreated);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "شمارنده با موفقیت به روز رسانی شد", Value = new { }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] DeleteCounter deleteCounter)
        {
            try
            {
                if (deleteCounter.Id == 0 || deleteCounter.Id == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه شمارنده نادرستی فرستاده شده است", Value = new { }, Error = new { } });
                }
                else
                {
                    Counter CounterCreated = _service.Counter.GetById(deleteCounter.Id);
                    if (CounterCreated == null)
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شمارندای با این شناسه وجود ندارد", Value = new { }, Error = new { } });
                    }
                    CounterCreated.IsDelete = true;
                    _service.Counter.Edit(CounterCreated);
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "شمارنده با موفقیت حذف شد", Value = new { }, Error = new { } });
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
                List<ShowCounters> res = _service.Counter.GetAll(languageId);
                List<KeyValuePair<string, List<ShowCounters>>> keyValuePair = new List<KeyValuePair<string, List<ShowCounters>>>();
                var languages = _service.Language.GetLanguages();
                foreach (var item in languages)
                {
                    keyValuePair.Add(new KeyValuePair<string, List<ShowCounters>>(item.Code, res.Where(w => w.LanguageId == item.Id).ToList()));
                }
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "شمارنده ها با موفقیت ارسال شد", Value = new { response = keyValuePair.ToDictionary(kvp => kvp.Key, kvp => kvp.Value) }, Error = new { } });
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
                List<BOShowCounters> res = _service.Counter.BOGetAll(languageId);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "شمارنده ها با موفقیت ارسال شد", Value = new { response = res }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
    }
}
