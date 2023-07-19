using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using ViraCMSBackend.Core.Helpers;
using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Domain.Models;
using ViraCMSBackend.Service.Base;
using static ViraCMSBackend.Domain.DTOs.BlogDTO;
using static ViraCMSBackend.Domain.DTOs.PartnerCompanyDTO;

namespace ViraCMSBackend.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class PartnerCompanyController : Controller
    {
        IServiceWrapper _service;
        private readonly AppSettings _appSettings;
        public PartnerCompanyController(IServiceWrapper service, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _service = service;
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody] AddPartnerCompany addPartnerCompany)
        {
            try
            {
                if (addPartnerCompany == null)
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
                if (addPartnerCompany.LanguageId == null || addPartnerCompany.LanguageId == 0)
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
                if (string.IsNullOrEmpty(addPartnerCompany.Name))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "نام شرکت مرتبط فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(addPartnerCompany.ThumbPhoto) || string.IsNullOrEmpty(addPartnerCompany.ThumbPhotoName))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "تامپ شرکت مرتبط فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(addPartnerCompany.HLink))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "لینک شرکت مرتبط فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                var thumbPhoto = _service.Picture.Upload(DateTime.Now.ToString("MMddHHmmss") + "-" + addPartnerCompany.ThumbPhotoName, addPartnerCompany.ThumbPhoto, false, 8);
                PartnerCompany partnerCompanyCreated = new PartnerCompany()
                {
                    LanguageId = addPartnerCompany.LanguageId,
                    Name = addPartnerCompany.Name.Trim().Replace("  ", " "),
                    ThumbPhoto = thumbPhoto.Address,
                    HLink = addPartnerCompany.Name.Trim(),
                };
                _service.PartnerCompany.Add(partnerCompanyCreated);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "شرکت مرتبط با موفقیت ثبت شد", Value = new { }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("Edit")]
        public IActionResult Edit([FromBody] EditPartnerCompany editPartnerCompany)
        {
            try
            {
                if (editPartnerCompany == null)
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
                if (editPartnerCompany.Id == null || editPartnerCompany.Id == 0)
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

                PartnerCompany partnerCompanyCreated = _service.PartnerCompany.GetById(editPartnerCompany.Id);
                if (partnerCompanyCreated == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "شرکت مرتبطی با این شناسه وجود ندارد",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (editPartnerCompany.LanguageId == null || editPartnerCompany.LanguageId == 0)
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
                if (string.IsNullOrEmpty(editPartnerCompany.Name))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "نام شرکت مرتبط فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(editPartnerCompany.ThumbPhoto) || string.IsNullOrEmpty(editPartnerCompany.ThumbPhotoName))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "تامپ شرکت مرتبط فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (editPartnerCompany.ThumbPhoto == partnerCompanyCreated.ThumbPhoto)
                {
                    partnerCompanyCreated.ThumbPhoto = partnerCompanyCreated.ThumbPhoto;
                }
                if (editPartnerCompany.ThumbPhoto != partnerCompanyCreated.ThumbPhoto)
                {
                    var res = _service.Picture.GetByAddress(editPartnerCompany.ThumbPhoto);
                    if (res != null)
                    {
                        var imageName = res.Thumbnail.Split("/")[res.Thumbnail.Split("/").Count() - 1];
                        if (System.IO.File.Exists(_appSettings.SaveImagePath + "\\PartnerCompanies\\" + imageName))
                            System.IO.File.Delete(_appSettings.SaveImagePath + "\\PartnerCompanies\\" + imageName);
                        _service.Picture.DeleteById(res.Id);
                    }
                    var thumbPhoto = _service.Picture.Upload(DateTime.Now.ToString("MMddHHmmss") + "-" + editPartnerCompany.ThumbPhotoName, editPartnerCompany.ThumbPhoto, false, 8);
                    partnerCompanyCreated.ThumbPhoto = thumbPhoto.Address;
                }
                if (string.IsNullOrEmpty(editPartnerCompany.HLink))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "لینک شرکت مرتبط فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                partnerCompanyCreated.LanguageId = editPartnerCompany.LanguageId.GetValueOrDefault();
                partnerCompanyCreated.Name = editPartnerCompany.Name.Trim().Replace("  ", " ");
                partnerCompanyCreated.HLink = editPartnerCompany.HLink.Trim();
                partnerCompanyCreated.IsActive = editPartnerCompany.IsActive;
                _service.PartnerCompany.Edit(partnerCompanyCreated);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "شرکت مرتبط با موفقیت به روز رسانی شد", Value = new { }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] DeletePartnerCompany deletePartnerCompany)
        {
            try
            {
                if (deletePartnerCompany.Id == 0 || deletePartnerCompany.Id == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه شرکت مرتط نادرستی فرستاده شده است", Value = new { }, Error = new { } });
                }
                else
                {
                    PartnerCompany PartnerCompanyCreated = _service.PartnerCompany.GetById(deletePartnerCompany.Id);
                    if (PartnerCompanyCreated == null)
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شرکت مرتبطی با این شناسه وجود ندارد", Value = new { }, Error = new { } });
                    }
                    PartnerCompanyCreated.IsDelete = true;
                    _service.PartnerCompany.Edit(PartnerCompanyCreated);
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "شرکت مرتبط با موفقیت حذف شد", Value = new { }, Error = new { } });
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
                List<ShowPartnerCompanies> res = _service.PartnerCompany.GetAll(languageId);
                List<KeyValuePair<string, List<ShowPartnerCompanies>>> keyValuePair = new List<KeyValuePair<string, List<ShowPartnerCompanies>>>();
                var languages = _service.Language.GetLanguages();
                foreach (var item in languages)
                {
                    keyValuePair.Add(new KeyValuePair<string, List<ShowPartnerCompanies>>(item.Code, res.Where(w => w.LanguageId == item.Id).ToList()));
                }
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "شرکت های مرتبط با موفقیت ارسال شد", Value = new { response = keyValuePair.ToDictionary(kvp => kvp.Key, kvp => kvp.Value) }, Error = new { } });
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
                List<BOShowPartnerCompanies> res = _service.PartnerCompany.BOGetAll(languageId);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "شرکت های مرتبط با موفقیت ارسال شد", Value = new { response = res }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
    }
}
