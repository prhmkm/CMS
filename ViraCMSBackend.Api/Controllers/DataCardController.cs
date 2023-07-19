using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using ViraCMSBackend.Core.Helpers;
using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Domain.Models;
using ViraCMSBackend.Service.Base;
using static ViraCMSBackend.Domain.DTOs.DataCardDTO;

namespace ViraCMSBackend.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class DataCardController : Controller
    {
        IServiceWrapper _service;
        private readonly AppSettings _appSettings;
        public DataCardController(IServiceWrapper service, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _service = service;
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody] AddDataCardRequest addDataCardRequest)
        {
            try
            {
                if (addDataCardRequest == null)
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
                if (addDataCardRequest.LanguageId == null || addDataCardRequest.LanguageId == 0)
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
                if (string.IsNullOrEmpty(addDataCardRequest.Title))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "عنوان کارت داده فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(addDataCardRequest.Image))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "عکس کارت داده فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(addDataCardRequest.Description))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "توضیحات کارت داده فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                var image = _service.Picture.Upload(DateTime.Now.ToString("MMddHHmmss") + "-" + addDataCardRequest.ImageName, addDataCardRequest.ImageName, false, 4);
                DataCard DataCardCreated = new DataCard()
                {
                    LanguageId = addDataCardRequest.LanguageId,
                    Title = addDataCardRequest.Title.Trim().Replace("  ", " "),
                    Image = image.Address,
                    Description = addDataCardRequest.Description.Trim(),
                };
                _service.DataCard.Add(DataCardCreated);
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "کارت داده با موفقیت ثبت شد",
                    Value = new { },
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
        [HttpPost("Edit")]
        public IActionResult Edit([FromBody] EditDataCardRequest editDataCardRequest)
        {
            try
            {
                if (editDataCardRequest == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "اطلاعات فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (editDataCardRequest.Id == null || editDataCardRequest.Id == 0)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه کارت داده فرستاده نشده است", Value = new { }, Error = new { } });
                }

                DataCard DataCardCreated = _service.DataCard.GetById(editDataCardRequest.Id);
                if (DataCardCreated == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "کارت دادهیی با این شناسه وجود ندارد", Value = new { }, Error = new { } });
                }
                if (editDataCardRequest.LanguageId == null || editDataCardRequest.LanguageId == 0)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه زبان فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(editDataCardRequest.Title))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "عنوان کارت داده فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(editDataCardRequest.Image))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "عکس کارت داده فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (editDataCardRequest.Image == DataCardCreated.Image)
                {
                    DataCardCreated.Image = DataCardCreated.Image;
                }
                if (editDataCardRequest.Image != DataCardCreated.Image)
                {
                    var res = _service.Picture.GetByAddress(DataCardCreated.Image);
                    if (res != null)
                    {
                        var imageName = res.Address.Split("/")[res.Address.Split("/").Count() - 1];
                        if (System.IO.File.Exists(_appSettings.SaveImagePath + "\\DataCards\\" + imageName))
                            System.IO.File.Delete(_appSettings.SaveImagePath + "\\DataCards\\" + imageName);
                        _service.Picture.DeleteById(res.Id);
                    }
                    var image = _service.Picture.Upload(DateTime.Now.ToString("MMddHHmmss") + "-" + editDataCardRequest.ImageName, editDataCardRequest.Image, false, 4);
                    DataCardCreated.Image = image.Address;
                }
                if (string.IsNullOrEmpty(editDataCardRequest.Description))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "توضیحات کارت داده فرستاده نشده است", Value = new { }, Error = new { } });
                }
                DataCardCreated.LanguageId = editDataCardRequest.LanguageId.GetValueOrDefault();
                DataCardCreated.Title = editDataCardRequest.Title.Trim().Replace("  ", " ");
                DataCardCreated.Description = editDataCardRequest.Description.Trim();
                DataCardCreated.IsActive = editDataCardRequest.IsActive;
                _service.DataCard.Edit(DataCardCreated);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "کارت داده با موفقیت به روز رسانی شد", Value = new { }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] DeleteDataCard deleteDataCard)
        {
            try
            {
                if (deleteDataCard.Id == 0 || deleteDataCard.Id == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه کارت داده نادرستی فرستاده شده است", Value = new { }, Error = new { } });
                }
                else
                {
                    DataCard DataCardCreated = _service.DataCard.GetById(deleteDataCard.Id);
                    if (DataCardCreated == null)
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "کارت دادهیی با این شناسه وجود ندارد", Value = new { }, Error = new { } });
                    }
                    DataCardCreated.IsDelete = true;
                    _service.DataCard.Edit(DataCardCreated);
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "کارت داده با موفقیت حذف شد", Value = new { }, Error = new { } });
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
                List<ShowDataCards> res = _service.DataCard.GetAll(languageId);
                List<KeyValuePair<string, List<ShowDataCards>>> keyValuePair = new List<KeyValuePair<string, List<ShowDataCards>>>();
                var languages = _service.Language.GetLanguages();
                foreach (var item in languages)
                {
                    keyValuePair.Add(new KeyValuePair<string, List<ShowDataCards>>(item.Code, res.Where(w => w.LanguageId == item.Id).ToList()));
                }
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "کارت داده ها با موفقیت ارسال شد", Value = new { response = keyValuePair.ToDictionary(kvp => kvp.Key, kvp => kvp.Value) }, Error = new { } });
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
                List<BOShowDataCards> res = _service.DataCard.BOGetAll(languageId);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "کارت داده ها با موفقیت ارسال شد", Value = new { response = res }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
    }
}
