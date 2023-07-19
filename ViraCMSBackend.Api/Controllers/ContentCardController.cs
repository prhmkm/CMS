using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using ViraCMSBackend.Core.Helpers;
using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Domain.Models;
using ViraCMSBackend.Service.Base;
using static ViraCMSBackend.Domain.DTOs.ContentCardDTO;

namespace ViraCMSBackend.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ContentCardController : Controller
    {
        IServiceWrapper _service;
        private readonly AppSettings _appSettings;
        public ContentCardController(IServiceWrapper service, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _service = service;
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody] AddContentCardRequest addContentCardRequest)
        {
            try
            {
                if (addContentCardRequest == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "اطلاعات فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (addContentCardRequest.LanguageId == null || addContentCardRequest.LanguageId == 0)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه زبان فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(addContentCardRequest.Title))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "عنوان کارت محتوا فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(addContentCardRequest.Image))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "عکس کارت محتوا فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(addContentCardRequest.Description))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "توضیحات کارت محتوا فرستاده نشده است", Value = new { }, Error = new { } });
                }
                var image = _service.Picture.Upload(DateTime.Now.ToString("MMddHHmmss") + "-" + addContentCardRequest.ImageName, addContentCardRequest.Image, false, 6);
                ContentCard ContentCardCreated = new ContentCard()
                {
                    LanguageId = addContentCardRequest.LanguageId,
                    Title = addContentCardRequest.Title.Trim().Replace("  ", " "),
                    Image = image.Address,
                    Description = addContentCardRequest.Description.Trim(),
                };
                _service.ContentCard.Add(ContentCardCreated);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "کارت محتوا با موفقیت ثبت شد", Value = new { }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("Edit")]
        public IActionResult Edit([FromBody] EditContentCardRequest editContentCardRequest)
        {
            try
            {
                if (editContentCardRequest == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "اطلاعات فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (editContentCardRequest.Id == null || editContentCardRequest.Id == 0)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه کارت محتوا فرستاده نشده است", Value = new { }, Error = new { } });
                }

                ContentCard ContentCardCreated = _service.ContentCard.GetById(editContentCardRequest.Id);
                if (ContentCardCreated == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "کارت محتوایی با این شناسه وجود ندارد", Value = new { }, Error = new { } });
                }
                if (editContentCardRequest.LanguageId == null || editContentCardRequest.LanguageId == 0)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه زبان فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(editContentCardRequest.Title))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "عنوان کارت محتوا فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(editContentCardRequest.Image))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "عکس کارت محتوا فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (editContentCardRequest.Image == ContentCardCreated.Image)
                {
                    ContentCardCreated.Image = ContentCardCreated.Image;
                }
                if (editContentCardRequest.Image != ContentCardCreated.Image)
                {
                    var res = _service.Picture.GetByAddress(ContentCardCreated.Image);
                    if (res != null)
                    {
                        var imageName = res.Address.Split("/")[res.Address.Split("/").Count() - 1];
                        if (System.IO.File.Exists(_appSettings.SaveImagePath + "\\ContentCards\\" + imageName))
                            System.IO.File.Delete(_appSettings.SaveImagePath + "\\ContentCards\\" + imageName);
                        _service.Picture.DeleteById(res.Id);
                    }
                    var image = _service.Picture.Upload(DateTime.Now.ToString("MMddHHmmss") + "-" + editContentCardRequest.ImageName, editContentCardRequest.Image, false, 6);
                    ContentCardCreated.Image = image.Address;
                }
                if (string.IsNullOrEmpty(editContentCardRequest.Description))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "توضیحات کارت محتوا فرستاده نشده است", Value = new { }, Error = new { } });
                }
                ContentCardCreated.LanguageId = editContentCardRequest.LanguageId.GetValueOrDefault();
                ContentCardCreated.Title = editContentCardRequest.Title.Trim().Replace("  ", " ");
                ContentCardCreated.Description = editContentCardRequest.Description.Trim();
                ContentCardCreated.IsActive = editContentCardRequest.IsActive;
                _service.ContentCard.Edit(ContentCardCreated);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "کارت محتوا با موفقیت به روز رسانی شد", Value = new { }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] DeleteContentCard deleteContentCard)
        {
            try
            {
                if (deleteContentCard.Id == 0 || deleteContentCard.Id == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه کارت محتوا نادرستی فرستاده شده است", Value = new { }, Error = new { } });
                }
                else
                {
                    ContentCard ContentCardCreated = _service.ContentCard.GetById(deleteContentCard.Id);
                    if (ContentCardCreated == null)
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "کارت محتوایی با این شناسه وجود ندارد", Value = new { }, Error = new { } });
                    }
                    ContentCardCreated.IsDelete = true;
                    _service.ContentCard.Edit(ContentCardCreated);
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "کارت محتوا با موفقیت حذف شد", Value = new { }, Error = new { } });
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
                List<ShowContentCards> res = _service.ContentCard.GetAll(languageId);
                List<KeyValuePair<string, List<ShowContentCards>>> keyValuePair = new List<KeyValuePair<string, List<ShowContentCards>>>();
                var languages = _service.Language.GetLanguages();
                foreach (var item in languages)
                {
                    keyValuePair.Add(new KeyValuePair<string, List<ShowContentCards>>(item.Code, res.Where(w => w.LanguageId == item.Id).ToList()));
                }
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "کارت محتوا ها با موفقیت ارسال شد", Value = new { response = keyValuePair.ToDictionary(kvp => kvp.Key, kvp => kvp.Value) }, Error = new { } });
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
                List<BOShowContentCards> res = _service.ContentCard.BOGetAll(languageId);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "کارت محتوا ها با موفقیت ارسال شد", Value = new { response = res }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
    }
}
