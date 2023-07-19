using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using ViraCMSBackend.Core.Helpers;
using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Domain.Models;
using ViraCMSBackend.Service.Base;
using static ViraCMSBackend.Domain.DTOs.MenuDTO;

namespace ViraCMSBackend.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class MenuController : Controller
    {
        IServiceWrapper _service;
        private readonly AppSettings _appSettings;
        public MenuController(IServiceWrapper service, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _service = service;
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody] AddMenuRequest addMenuRequest)
        {
            try
            {
                if (addMenuRequest == null)
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

                if (addMenuRequest.LanguageId == null || addMenuRequest.LanguageId == 0)
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
                if (string.IsNullOrEmpty(addMenuRequest.Title))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "عنوان منو فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (addMenuRequest.ParentId == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "شناسه والد منو فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (!string.IsNullOrEmpty(addMenuRequest.HLink) && addMenuRequest.PageId != null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "هر دو لینک و شناسه صفحه منو فرستاده شده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (addMenuRequest.Ordering == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "شماره ترتیب منو فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (_service.Menu.GetByOrderAndParent(addMenuRequest.Ordering, addMenuRequest.ParentId, addMenuRequest.LanguageId).Item1 == 1)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "شماره ترتیب منو نامعتبر است",
                        Value = new { },
                        Error = new { }
                    });
                }
                Menu MenuCreated = new Menu()
                {
                    LanguageId = addMenuRequest.LanguageId,
                    Title = addMenuRequest.Title.Trim().Replace("  ", " "),
                    ParentId = addMenuRequest.ParentId,
                    HLink = addMenuRequest.HLink,
                    PageId = addMenuRequest.PageId,
                    Ordering = addMenuRequest.Ordering,
                };
                int id = _service.Menu.Add(MenuCreated);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "منو با موفقیت ثبت شد", Value = new { id = id }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.InternalServerError,
                    Message = "خطای داخلی سرور رخ داده است",
                    Value = new { },
                    Error = new
                    {
                        Response = ex.ToString()
                    }
                });
            }
        }
        [HttpPost("Edit")]
        public IActionResult Edit([FromBody] EditMenuRequest editMenuRequest)
        {
            try
            {
                if (editMenuRequest == null)
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
                if (editMenuRequest.Id == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "شناسه منو فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                Menu MenuCreated = _service.Menu.GetById(editMenuRequest.Id);
                if (MenuCreated == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "منویی با این شناسه وجود ندارد",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (editMenuRequest.LanguageId == null || editMenuRequest.LanguageId == 0)
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
                if (string.IsNullOrEmpty(editMenuRequest.Title))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "عنوان منو فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (editMenuRequest.ParentId == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "شناسه والد منو فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (!string.IsNullOrEmpty(editMenuRequest.HLink) && editMenuRequest.PageId != null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "هر دو لینک و شناسه صفحه منو فرستاده شده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (editMenuRequest.Ordering == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "شماره ترتیب منو فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                var x = _service.Menu.GetByOrderAndParent(editMenuRequest.Ordering.GetValueOrDefault(), editMenuRequest.ParentId.GetValueOrDefault(), editMenuRequest.LanguageId.GetValueOrDefault());
                if (x.Item1 == 1 && x.Item2.Id != MenuCreated.Id)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "شماره ترتیب منو نامعتبر است",
                        Value = new { },
                        Error = new { }
                    });
                }

                MenuCreated.LanguageId = editMenuRequest.LanguageId.GetValueOrDefault();
                MenuCreated.Title = editMenuRequest.Title.Trim().Replace("  ", " ");
                MenuCreated.ParentId = editMenuRequest.ParentId.GetValueOrDefault();
                MenuCreated.HLink = editMenuRequest.HLink;
                MenuCreated.PageId = editMenuRequest.PageId;
                MenuCreated.Ordering = editMenuRequest.Ordering.GetValueOrDefault();
                _service.Menu.Edit(MenuCreated);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "منو با موفقیت به روز رسانی شد", Value = new { }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] DeleteMenu deleteMenu)
        {
            try
            {
                if (deleteMenu.Id == 0 || deleteMenu.Id == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه منو نادرستی فرستاده شده است", Value = new { }, Error = new { } });
                }
                else
                {
                    Menu MenuCreated = _service.Menu.GetById(deleteMenu.Id);
                    if (MenuCreated == null)
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "منویی با این شناسه وجود ندارد", Value = new { }, Error = new { } });
                    }
                    if (_service.Menu.HaveChild(deleteMenu.Id) == false)
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "برای این منو زیرمجموعه وجود دارد", Value = new { }, Error = new { } });
                    }
                    MenuCreated.IsDelete = true;
                    _service.Menu.Edit(MenuCreated);
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "منو با موفقیت حذف شد", Value = new { }, Error = new { } });
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
                List<ShowMenu> res = _service.Menu.GetAll(0,languageId);
                List<KeyValuePair<string, List<ShowMenu>>> keyValuePair = new List<KeyValuePair<string, List<ShowMenu>>>();
                var languages = _service.Language.GetLanguages();
                foreach (var item in languages)
                {
                    keyValuePair.Add(new KeyValuePair<string, List<ShowMenu>>(item.Code, res.Where(w => w.LanguageId == item.Id).ToList()));
                }
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "منو با موفقیت ارسال شد", Value = new { response = keyValuePair.ToDictionary(kvp => kvp.Key, kvp => kvp.Value) }, Error = new { } });
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
                List<BOShowMenu> res = _service.Menu.BOGetAll(0,languageId);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "منو ها با موفقیت ارسال شد", Value = new { response = res }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }

    }
}
