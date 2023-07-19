using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using ViraCMSBackend.Core.Helpers;
using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Domain.Models;
using ViraCMSBackend.Service.Base;
using static ViraCMSBackend.Domain.DTOs.BlogDTO;

namespace ViraCMSBackend.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class BlogController : Controller
    {
        IServiceWrapper _service;
        private readonly AppSettings _appSettings;
        public BlogController(IServiceWrapper service, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _service = service;
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody] AddBlogRequest addBlogRequest)
        {
            try
            {
                if (addBlogRequest == null)
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
                if (addBlogRequest.LanguageId == null || addBlogRequest.LanguageId == 0)
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
                if (string.IsNullOrEmpty(addBlogRequest.Title))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "عنوان بلاگ فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (_service.Blog.GetByAlias(addBlogRequest.Title.Trim().Replace("  ", " ").Replace(" ", "-").Replace("\\", "-").Replace("/", "-").Replace("+", "")) != null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "عنوان بلاگ تکراری است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(addBlogRequest.ShortText))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "متن کوتاه بلاگ فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(addBlogRequest.ThumbPhoto) || string.IsNullOrEmpty(addBlogRequest.ThumbPhotoName))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "تامپ بلاگ فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(addBlogRequest.CustomHTML))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "جزئیات بلاگ فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                var thumbPhoto = _service.Picture.Upload(DateTime.Now.ToString("MMddHHmmss") + "-" + addBlogRequest.ThumbPhotoName, addBlogRequest.ThumbPhoto, true, 7);
                Blog BlogCreated = new Blog()
                {
                    LanguageId = addBlogRequest.LanguageId,
                    Title = addBlogRequest.Title.Trim().Replace("  ", " "),
                    Alias = addBlogRequest.Title.Trim().Replace("  ", " ").Replace(" ", "-").Replace("\\", "-").Replace("/", "-").Replace("+", ""),
                    ShortText = addBlogRequest.ShortText.Trim(),
                    ThumbPhoto = thumbPhoto.ThumpAddress,
                    CustomHTML = addBlogRequest.CustomHTML.Trim(),
                };
                _service.Blog.Add(BlogCreated);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "بلاگ با موفقیت ثبت شد", Value = new { }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("Edit")]
        public IActionResult Edit([FromBody] EditBlogRequest editBlogRequest)
        {
            try
            {
                if (editBlogRequest == null)
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
                if (editBlogRequest.Id == null || editBlogRequest.Id == 0)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "شناسه بلاگ فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }

                Blog BlogCreated = _service.Blog.GetById(editBlogRequest.Id);
                if (BlogCreated == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "بلاگی با این شناسه وجود ندارد",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (editBlogRequest.LanguageId == null || editBlogRequest.LanguageId == 0)
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
                if (string.IsNullOrEmpty(editBlogRequest.Title))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "عنوان بلاگ فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                var x = _service.Blog.GetByAlias(editBlogRequest.Title.Trim().Replace("  ", " ").Replace(" ", "-").Replace("\\", "-").Replace("/", "-").Replace("+", ""));
                if (x != null && x.Id != editBlogRequest.Id)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "عنوان بلاگ تکراری است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(editBlogRequest.ShortText))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "متن کوتاه بلاگ فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(editBlogRequest.ThumbPhoto))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "عکس تامپ بلاگ فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (editBlogRequest.ThumbPhoto == BlogCreated.ThumbPhoto)
                {
                    BlogCreated.ThumbPhoto = BlogCreated.ThumbPhoto;
                }
                if (editBlogRequest.ThumbPhoto != BlogCreated.ThumbPhoto)
                {
                    var res = _service.Picture.GetByAddress(BlogCreated.ThumbPhoto);
                    if (res != null)
                    {
                        var imageName = res.Thumbnail.Split("/")[res.Thumbnail.Split("/").Count() - 1];
                        if (System.IO.File.Exists(_appSettings.SaveImagePath + "\\Blogs\\" + imageName))
                            System.IO.File.Delete(_appSettings.SaveImagePath + "\\Blogs\\" + imageName);
                        _service.Picture.DeleteById(res.Id);
                    }
                    var thumbPhoto = _service.Picture.Upload(DateTime.Now.ToString("MMddHHmmss") + "-" + editBlogRequest.ThumbPhotoName, editBlogRequest.ThumbPhoto, true, 7);
                    BlogCreated.ThumbPhoto = thumbPhoto.ThumpAddress;
                }
                if (string.IsNullOrEmpty(editBlogRequest.CustomHTML))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "جزئیات بلاگ فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                BlogCreated.LanguageId = editBlogRequest.LanguageId.GetValueOrDefault();
                BlogCreated.Title = editBlogRequest.Title.Trim().Replace("  ", " ");
                BlogCreated.Alias = editBlogRequest.Title.Trim().Replace("  ", " ").Replace(" ", "-").Replace("\\", "-").Replace("/", "-").Replace("+", "");
                BlogCreated.ShortText = editBlogRequest.ShortText.Trim();
                BlogCreated.CustomHTML = editBlogRequest.CustomHTML.Trim();
                BlogCreated.IsActive = editBlogRequest.IsActive;
                _service.Blog.Edit(BlogCreated);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "بلاگ با موفقیت به روز رسانی شد", Value = new { }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] DeleteBlog deleteBlog)
        {
            try
            {
                if (deleteBlog.Id == 0 || deleteBlog.Id == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه بلاگ نادرستی فرستاده شده است", Value = new { }, Error = new { } });
                }
                else
                {
                    Blog BlogCreated = _service.Blog.GetById(deleteBlog.Id);
                    if (BlogCreated == null)
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "بلاگی با این شناسه وجود ندارد", Value = new { }, Error = new { } });
                    }
                    BlogCreated.IsDelete = true;
                    _service.Blog.Edit(BlogCreated);
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "بلاگ با موفقیت حذف شد", Value = new { }, Error = new { } });
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
                List<ShowBlogs> res = _service.Blog.GetAll(languageId);
                List<KeyValuePair<string, List<ShowBlogs>>> keyValuePair = new List<KeyValuePair<string, List<ShowBlogs>>>();
                var languages = _service.Language.GetLanguages();
                foreach (var item in languages)
                {
                    keyValuePair.Add(new KeyValuePair<string, List<ShowBlogs>>(item.Code, res.Where(w => w.LanguageId == item.Id).ToList()));
                }
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "بلاگ ها با موفقیت ارسال شد", Value = new { response = keyValuePair.ToDictionary(kvp => kvp.Key, kvp => kvp.Value) }, Error = new { } });
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
                List<BOShowBlogs> res = _service.Blog.BOGetAll(languageId);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "بلاگ ها با موفقیت ارسال شد", Value = new { response = res }, Error = new { } });
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
                Blog res = _service.Blog.GetById(id);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "بلاگ با موفقیت ارسال شد", Value = new { response = res }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [AllowAnonymous]
        [HttpPost("GetByAlias")]
        public IActionResult GetByAlias([FromBody] GetBlogByAlias Alias)
        {
            try
            {
                Blog res = _service.Blog.GetByAlias(Alias.Alias);
                if (res == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "بلاگی با این شناسه وجود ندارد",
                        Value = new { },
                        Error = new { }
                    });
                }
                res.ViewCounter++;
                _service.Blog.Edit(res);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "بلاگ با موفقیت ارسال شد", Value = new { response = res }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
    }
}
