using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using ViraCMSBackend.Core.Helpers;
using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Domain.Models;
using ViraCMSBackend.Service.Base;
using static ViraCMSBackend.Domain.DTOs.NewsDTO;

namespace ViraCMSBackend.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class NewsController : Controller
    {
        IServiceWrapper _service;
        private readonly AppSettings _appSettings;
        public NewsController(IServiceWrapper service, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _service = service;
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody] AddNewsRequest addNewsRequest)
        {
            try
            {
                if (addNewsRequest == null)
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
                if (addNewsRequest.LanguageId == null || addNewsRequest.LanguageId == 0)
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
                if (string.IsNullOrEmpty(addNewsRequest.Title))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "عنوان خبر فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (_service.News.GetByAlias(addNewsRequest.Title.Trim().Replace("  ", " ").Replace(" ", "-").Replace("\\", "-").Replace("/", "-").Replace("+", "")) != null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "عنوان خبر تکراری است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(addNewsRequest.ShortText))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "متن کوتاه خبر فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(addNewsRequest.ThumbPhoto))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "تامپ خبر فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(addNewsRequest.CustomHTML))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "جزئیات خبر فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                var thumbPhoto = _service.Picture.Upload(DateTime.Now.ToString("MMddHHmmss") + "-" + addNewsRequest.ThumbPhotoName, addNewsRequest.ThumbPhoto, true, 2);
                News NewsCreated = new News()
                {
                    LanguageId = addNewsRequest.LanguageId,
                    Title = addNewsRequest.Title.Trim().Replace("  ", " "),
                    Alias = addNewsRequest.Title.Trim().Replace("  ", " ").Replace(" ", "-").Replace("\\", "-").Replace("/", "-").Replace("+", ""),
                    ShortText = addNewsRequest.ShortText.Trim(),
                    ThumbPhoto = thumbPhoto.ThumpAddress,
                    CustomHTML = addNewsRequest.CustomHTML.Trim(),
                };
                _service.News.Add(NewsCreated);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "خبر با موفقیت ثبت شد", Value = new { }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("Edit")]
        public IActionResult Edit([FromBody] EditNewsRequest editNewsRequest)
        {
            try
            {
                if (editNewsRequest == null)
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
                if (editNewsRequest.Id == null || editNewsRequest.Id == 0)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "شناسه خبر نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }

                News NewsCreated = _service.News.GetById(editNewsRequest.Id);
                if (NewsCreated == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "خبری با این شناسه وجود ندارد",
                        Value = new { },
                        Error = new { }
                    });
                }

                if (editNewsRequest.LanguageId == null || editNewsRequest.LanguageId == 0)
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
                if (string.IsNullOrEmpty(editNewsRequest.Title))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "عنوان خبر فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                var x = _service.News.GetByAlias(editNewsRequest.Title.Trim().Replace("  ", " ").Replace(" ", "-").Replace("\\", "-").Replace("/", "-").Replace("+", ""));
                if (x != null && x.Id != editNewsRequest.Id)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "عنوان خبر تکراری است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(editNewsRequest.ShortText))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "متن کوتاه خبر فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(editNewsRequest.ThumbPhoto))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "عکس تامپ خبر فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (editNewsRequest.ThumbPhoto == NewsCreated.ThumbPhoto)
                {
                    NewsCreated.ThumbPhoto = NewsCreated.ThumbPhoto;
                }
                if (editNewsRequest.ThumbPhoto != NewsCreated.ThumbPhoto)
                {
                    var res = _service.Picture.GetByAddress(NewsCreated.ThumbPhoto);
                    if (res != null)
                    {
                        var imageName = res.Thumbnail.Split("/")[res.Thumbnail.Split("/").Count() - 1];
                        if (System.IO.File.Exists(_appSettings.SaveImagePath + "\\News\\" + imageName))
                            System.IO.File.Delete(_appSettings.SaveImagePath + "\\News\\" + imageName);
                        _service.Picture.DeleteById(res.Id);
                    }
                    var thumbPhoto = _service.Picture.Upload(DateTime.Now.ToString("MMddHHmmss") + "-" + editNewsRequest.ThumbPhotoName, editNewsRequest.ThumbPhoto, true, 2);
                    NewsCreated.ThumbPhoto = thumbPhoto.ThumpAddress;
                }
                if (string.IsNullOrEmpty(editNewsRequest.CustomHTML))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "جزئیات خبر فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                NewsCreated.LanguageId = editNewsRequest.LanguageId.GetValueOrDefault();
                NewsCreated.Title = editNewsRequest.Title.Trim().Replace("  ", " ");
                NewsCreated.Alias = editNewsRequest.Title.Trim().Replace("  ", " ").Replace(" ", "-").Replace("\\", "-").Replace("/", "-").Replace("+", "");
                NewsCreated.ShortText = editNewsRequest.ShortText.Trim();
                NewsCreated.CustomHTML = editNewsRequest.CustomHTML.Trim();
                NewsCreated.IsActive = editNewsRequest.IsActive;
                _service.News.Edit(NewsCreated);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "خبر با موفقیت به روز رسانی شد", Value = new { }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] DeleteNews deleteNews)
        {
            try
            {
                if (deleteNews.Id == 0 || deleteNews.Id == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه خبر نادرستی فرستاده شده است", Value = new { }, Error = new { } });
                }
                else
                {
                    News NewsCreated = _service.News.GetById(deleteNews.Id);
                    if (NewsCreated == null)
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "خبری با این شناسه وجود ندارد", Value = new { }, Error = new { } });
                    }
                    NewsCreated.IsDelete = true;
                    _service.News.Edit(NewsCreated);
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "خبر با موفقیت حذف شد", Value = new { }, Error = new { } });
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
                List<ShowNews> res = _service.News.GetAll(languageId);
                List<KeyValuePair<string, List<ShowNews>>> keyValuePair = new List<KeyValuePair<string, List<ShowNews>>>();
                var languages = _service.Language.GetLanguages();
                foreach (var item in languages)
                {
                    keyValuePair.Add(new KeyValuePair<string, List<ShowNews>>(item.Code, res.Where(w => w.LanguageId == item.Id).ToList()));
                }
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "خبر ها با موفقیت ارسال شد", Value = new { response = keyValuePair.ToDictionary(kvp => kvp.Key, kvp => kvp.Value) }, Error = new { } });
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
                List<BOShowNews> res = _service.News.BOGetAll(languageId);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "خبر ها با موفقیت ارسال شد", Value = new { response = res }, Error = new { } });
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
                News res = _service.News.GetById(id);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "خبر با موفقیت ارسال شد", Value = new { response = res }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [AllowAnonymous]
        [HttpPost("GetByAlias")]
        public IActionResult GetByAlias([FromBody] GetNewsByAlias Alias)
        {
            try
            {
                News res = _service.News.GetByAlias(Alias.Alias);
                if(res == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "خبری با این شناسه وجود ندارد",
                        Value = new { },
                        Error = new { }
                    });
                }
                res.ViewCounter++;
                _service.News.Edit(res);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "خبر با موفقیت ارسال شد", Value = new { response = res }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
    }
}
