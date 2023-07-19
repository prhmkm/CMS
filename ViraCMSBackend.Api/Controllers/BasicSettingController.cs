using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using ViraCMSBackend.Core.Helpers;
using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Domain.Models;
using ViraCMSBackend.Service.Base;
using static ViraCMSBackend.Domain.DTOs.BasicSettingDTO;

namespace ViraCMSBackend.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class BasicSettingController : Controller
    {
        IServiceWrapper _service;
        private readonly AppSettings _appSettings;
        public BasicSettingController(IServiceWrapper service, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _service = service;
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody] AddBasicSttingRequest addBasicSttingRequest)
        {
            try
            {
                if (addBasicSttingRequest == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "اطلاعات فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (addBasicSttingRequest.LanguageId == null || addBasicSttingRequest.LanguageId == 0)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه زبان فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(addBasicSttingRequest.Title))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "عنوان فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(addBasicSttingRequest.Address))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "آدرس فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(addBasicSttingRequest.WorkingHours))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "ساعت کاری فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(addBasicSttingRequest.PhoneNumber))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "تلفن ثابت فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (addBasicSttingRequest.PhoneNumber.Count() != 11)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "تلفن ثابت نامعتبر است", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(addBasicSttingRequest.MobileNumber))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "تلفن همراه فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (addBasicSttingRequest.MobileNumber.Count() != 11 || addBasicSttingRequest.MobileNumber.Substring(0, 2) != "09")
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "تلفن همراه نامعتبر است", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(addBasicSttingRequest.Fax))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "فکس فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(addBasicSttingRequest.FooterText))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "متن زیرین فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(addBasicSttingRequest.AboutUs))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "متن درباره ما فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (addBasicSttingRequest.IsActive == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "اکتیو بودن زبان فرستاده نشده است", Value = new { }, Error = new { } });
                }
                BasicSetting BasicSettingCreated = new BasicSetting()
                {
                    LanguageId = addBasicSttingRequest.LanguageId,
                    Title = addBasicSttingRequest.Title.Trim().Replace("  ", " "),
                    Address = addBasicSttingRequest.Address.Trim(),
                    PhoneNumber = addBasicSttingRequest.PhoneNumber,
                    MobileNumber = addBasicSttingRequest.MobileNumber,
                    WorkingHours = addBasicSttingRequest.WorkingHours,
                    Fax = addBasicSttingRequest.Fax,
                    FooterText = addBasicSttingRequest.FooterText.Trim(),
                    AboutUs = addBasicSttingRequest.AboutUs.Trim(),
                    IsActive = addBasicSttingRequest.IsActive,
                };
                _service.BasicSetting.Add(BasicSettingCreated);

                var lang = _service.Language.GetById(BasicSettingCreated.LanguageId);

                lang.IsActive = addBasicSttingRequest.IsActive;
                _service.Language.Edit(lang);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "تنظیمات اولیه با موفقیت ثبت شد", Value = new { }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("AddFixedSetting")]
        public IActionResult AddFixedSetting([FromBody] AddFixedBasicSttingRequest addBasicSttingRequest)
        {
            try
            {
                if (addBasicSttingRequest == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "اطلاعات فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (_service.FixedBasicSetting.GetAll() != null)
                {
                    FixedBasicSetting FixedBasicSettingCreated = _service.FixedBasicSetting.GetById(0);

                    if (FixedBasicSettingCreated == null)
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "تنظیمات اولیه با این شناسه وجود ندارد", Value = new { }, Error = new { } });
                    }
                    if (string.IsNullOrEmpty(addBasicSttingRequest.FavoriteIcon))
                    {
                        return Ok(new
                        {
                            TimeStamp = DateTime.Now,
                            ResponseCode = HttpStatusCode.BadRequest,
                            Message = "ایکون موردعلاقه فرستاده نشده است",
                            Value = new { },
                            Error = new { }
                        });
                    }
                    if (addBasicSttingRequest.FavoriteIcon == FixedBasicSettingCreated.FavoriteIcon)
                    {
                        FixedBasicSettingCreated.FavoriteIcon = FixedBasicSettingCreated.FavoriteIcon;
                    }
                    if (string.IsNullOrEmpty(addBasicSttingRequest.Logo))
                    {
                        return Ok(new
                        {
                            TimeStamp = DateTime.Now,
                            ResponseCode = HttpStatusCode.BadRequest,
                            Message = "لوگو فرستاده نشده است",
                            Value = new { },
                            Error = new { }
                        });
                    }
                    if (addBasicSttingRequest.Logo == FixedBasicSettingCreated.Logo)
                    {
                        FixedBasicSettingCreated.Logo = FixedBasicSettingCreated.Logo;
                    }
                    if (string.IsNullOrEmpty(addBasicSttingRequest.FooterLogo))
                    {
                        return Ok(new
                        {
                            TimeStamp = DateTime.Now,
                            ResponseCode = HttpStatusCode.BadRequest,
                            Message = "فوتر-لوگو فرستاده نشده است",
                            Value = new { },
                            Error = new { }
                        });
                    }
                    if (addBasicSttingRequest.FooterLogo == FixedBasicSettingCreated.FooterLogo)
                    {
                        FixedBasicSettingCreated.FooterLogo = FixedBasicSettingCreated.FooterLogo;
                    }
                    if (addBasicSttingRequest.FavoriteIcon != FixedBasicSettingCreated.FavoriteIcon)
                    {
                        var res = _service.Picture.GetByAddress(FixedBasicSettingCreated.FavoriteIcon);

                        if (res != null)
                        {
                            var imageName = res.Address.Split("/")[res.Address.Split("/").Count() - 1];
                            if (System.IO.File.Exists(_appSettings.SaveImagePath + "\\FixedBasicSettings\\" + imageName))
                                System.IO.File.Delete(_appSettings.SaveImagePath + "\\FixedBasicSettings\\" + imageName);
                            _service.Picture.DeleteById(res.Id);
                        }
                        var favoriteIcon = _service.Picture.Upload(DateTime.Now.ToString("MMddHHmmss") + "-" + addBasicSttingRequest.FavoriteIconName, addBasicSttingRequest.FavoriteIcon, false, 3);
                        FixedBasicSettingCreated.FavoriteIcon = favoriteIcon.Address;
                    }
                    if (addBasicSttingRequest.Logo != FixedBasicSettingCreated.Logo)
                    {
                        var res = _service.Picture.GetByAddress(FixedBasicSettingCreated.Logo);

                        if (res != null)
                        {
                            var imageName = res.Address.Split("/")[res.Address.Split("/").Count() - 1];
                            if (System.IO.File.Exists(_appSettings.SaveImagePath + "\\FixedBasicSettings\\" + imageName))
                                System.IO.File.Delete(_appSettings.SaveImagePath + "\\FixedBasicSettings\\" + imageName);
                            _service.Picture.DeleteById(res.Id);
                        }
                        var logo = _service.Picture.Upload(DateTime.Now.ToString("MMddHHmmss") + "-" + addBasicSttingRequest.LogoName, addBasicSttingRequest.Logo, false, 3);
                        FixedBasicSettingCreated.Logo = logo.Address;
                    }
                    if (addBasicSttingRequest.FooterLogo != FixedBasicSettingCreated.FooterLogo)
                    {
                        var res = _service.Picture.GetByAddress(FixedBasicSettingCreated.FooterLogo);
                        if (res != null)
                        {
                            var imageName = res.Address.Split("/")[res.Address.Split("/").Count() - 1];
                            if (System.IO.File.Exists(_appSettings.SaveImagePath + "\\FixedBasicSettings\\" + imageName))
                                System.IO.File.Delete(_appSettings.SaveImagePath + "\\FixedBasicSettings\\" + imageName);
                            _service.Picture.DeleteById(res.Id);
                        }
                        var footerLogo = _service.Picture.Upload(DateTime.Now.ToString("MMddHHmmss") + "-" + addBasicSttingRequest.FooterLogoName, addBasicSttingRequest.FooterLogo, false, 3);
                        FixedBasicSettingCreated.FooterLogo = footerLogo.Address;
                    }
                    if (string.IsNullOrEmpty(addBasicSttingRequest.FirstColor))
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه رنگ اول فرستاده نشده است", Value = new { }, Error = new { } });
                    }
                    if (string.IsNullOrEmpty(addBasicSttingRequest.SecondColor))
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه رنگ دوم فرستاده نشده است", Value = new { }, Error = new { } });
                    }
                    if (string.IsNullOrEmpty(addBasicSttingRequest.ThirdColor))
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه رنگ سوم فرستاده نشده است", Value = new { }, Error = new { } });
                    }
                    if (string.IsNullOrEmpty(addBasicSttingRequest.Email))
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "ایمیل فرستاده نشده است", Value = new { }, Error = new { } });
                    }

                    FixedBasicSettingCreated.FirstColor = addBasicSttingRequest.FirstColor;
                    FixedBasicSettingCreated.SecondColor = addBasicSttingRequest.SecondColor;
                    FixedBasicSettingCreated.ThirdColor = addBasicSttingRequest.ThirdColor;
                    FixedBasicSettingCreated.Email = addBasicSttingRequest.Email.Trim();
                    FixedBasicSettingCreated.TelegramAddress = addBasicSttingRequest.TelegramAddress.Trim();
                    FixedBasicSettingCreated.WhatsAppAddress = addBasicSttingRequest.WhatsAppAddress.Trim();
                    FixedBasicSettingCreated.InstagramAddress = addBasicSttingRequest.InstagramAddress.Trim();
                    FixedBasicSettingCreated.LinkedinAddress = addBasicSttingRequest.LinkedinAddress.Trim();

                    _service.FixedBasicSetting.Edit(FixedBasicSettingCreated);
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "تنظیمات اولیه ثابت با موفقیت به روز رسانی شد", Value = new { }, Error = new { } });
                }
                else
                {
                    if (string.IsNullOrEmpty(addBasicSttingRequest.FavoriteIcon) || string.IsNullOrEmpty(addBasicSttingRequest.FavoriteIconName))
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "ایکون موردعلاقه فرستاده نشده است", Value = new { }, Error = new { } });
                    }
                    if (string.IsNullOrEmpty(addBasicSttingRequest.Logo) || string.IsNullOrEmpty(addBasicSttingRequest.LogoName))
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "لوگو فرستاده نشده است", Value = new { }, Error = new { } });
                    }
                    if (string.IsNullOrEmpty(addBasicSttingRequest.FooterLogo) || string.IsNullOrEmpty(addBasicSttingRequest.FooterLogoName))
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "لوگو-فوتر فرستاده نشده است", Value = new { }, Error = new { } });
                    }
                    if (string.IsNullOrEmpty(addBasicSttingRequest.FirstColor))
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه رنگ اول فرستاده نشده است", Value = new { }, Error = new { } });
                    }
                    if (string.IsNullOrEmpty(addBasicSttingRequest.SecondColor))
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه رنگ دوم فرستاده نشده است", Value = new { }, Error = new { } });
                    }
                    if (string.IsNullOrEmpty(addBasicSttingRequest.ThirdColor))
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه رنگ سوم فرستاده نشده است", Value = new { }, Error = new { } });
                    }
                    if (string.IsNullOrEmpty(addBasicSttingRequest.Email))
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "ایمیل فرستاده نشده است", Value = new { }, Error = new { } });
                    }
                    var favoriteIcon = _service.Picture.Upload(DateTime.Now.ToString("MMddHHmmss") + "-" + addBasicSttingRequest.FavoriteIconName, addBasicSttingRequest.FavoriteIcon, false, 3);
                    var logo = _service.Picture.Upload(DateTime.Now.ToString("MMddHHmmss") + "-" + addBasicSttingRequest.LogoName, addBasicSttingRequest.Logo, false, 3);
                    var footerLogo = _service.Picture.Upload(DateTime.Now.ToString("MMddHHmmss") + "-" + addBasicSttingRequest.FooterLogoName, addBasicSttingRequest.FooterLogo, false, 3);
                    FixedBasicSetting FixedBasicSettingCreated = new FixedBasicSetting()
                    {
                        FavoriteIcon = favoriteIcon.Address,
                        Logo = logo.Address,
                        FooterLogo = footerLogo.Address,
                        FirstColor = addBasicSttingRequest.FirstColor,
                        SecondColor = addBasicSttingRequest.SecondColor,
                        ThirdColor = addBasicSttingRequest.ThirdColor,
                        Email = addBasicSttingRequest.Email.Trim(),
                        TelegramAddress = addBasicSttingRequest.TelegramAddress.Trim(),
                        InstagramAddress = addBasicSttingRequest.InstagramAddress.Trim(),
                        WhatsAppAddress = addBasicSttingRequest.WhatsAppAddress.Trim(),
                        LinkedinAddress = addBasicSttingRequest.LinkedinAddress.Trim(),
                    };
                    _service.FixedBasicSetting.Add(FixedBasicSettingCreated);
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "تنظیمات اولیه ثابت با موفقیت ثبت شد", Value = new { }, Error = new { } });
                }

            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("Edit")]
        public IActionResult Edit([FromBody] EditBasicSttingRequest editBasicSttingRequest)
        {
            try
            {
                if (editBasicSttingRequest == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "اطلاعات فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (editBasicSttingRequest.Id == null || editBasicSttingRequest.Id == 0)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه تنظیمات اولیه فرستاده نشده است", Value = new { }, Error = new { } });
                }
                BasicSetting BasicSettingCreated = _service.BasicSetting.GetById(editBasicSttingRequest.Id);
                if (BasicSettingCreated == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "تنظیمات اولیه با این شناسه وجود ندارد", Value = new { }, Error = new { } });
                }
                if (editBasicSttingRequest.LanguageId == null || editBasicSttingRequest.LanguageId == 0)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه زبان فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(editBasicSttingRequest.Title))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "عنوان فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(editBasicSttingRequest.Address))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "آدرس فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(editBasicSttingRequest.PhoneNumber))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "تلفن ثابت فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (editBasicSttingRequest.PhoneNumber.Count() != 11 || editBasicSttingRequest.PhoneNumber.Substring(0, 2) != "09")
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "تلفن ثابت نامعتبر است", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(editBasicSttingRequest.MobileNumber))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "تلفن همراه فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (editBasicSttingRequest.MobileNumber.Count() != 11 || editBasicSttingRequest.MobileNumber.Substring(0, 2) != "09")
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "تلفن همراه نامعتبر است", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(editBasicSttingRequest.WorkingHours))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "ساعت کاری فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(editBasicSttingRequest.Fax))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "فکس فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(editBasicSttingRequest.FooterText))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "متن زیرین فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(editBasicSttingRequest.AboutUs))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "متن درباره ما فرستاده نشده است", Value = new { }, Error = new { } });
                }
                BasicSettingCreated.LanguageId = editBasicSttingRequest.LanguageId.GetValueOrDefault();
                BasicSettingCreated.Title = editBasicSttingRequest.Title.Trim().Replace("  ", " ");
                BasicSettingCreated.Address = editBasicSttingRequest.Address.Trim();
                BasicSettingCreated.PhoneNumber = editBasicSttingRequest.PhoneNumber;
                BasicSettingCreated.MobileNumber = editBasicSttingRequest.MobileNumber;
                BasicSettingCreated.WorkingHours = editBasicSttingRequest.WorkingHours;
                BasicSettingCreated.Fax = editBasicSttingRequest.Fax;
                BasicSettingCreated.FooterText = editBasicSttingRequest.FooterText.Trim();
                BasicSettingCreated.AboutUs = editBasicSttingRequest.AboutUs.Trim();
                BasicSettingCreated.IsActive = editBasicSttingRequest.IsActive;

                _service.BasicSetting.Edit(BasicSettingCreated);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "تنظیمات اولیه با موفقیت به روز رسانی شد", Value = new { }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("EditFixedSetting")]
        public IActionResult EditFixedSetting([FromBody] EditFixedBasicSttingRequest editBasicSttingRequest)
        {
            try
            {
                if (editBasicSttingRequest == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "اطلاعات فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (editBasicSttingRequest.Id == null || editBasicSttingRequest.Id == 0)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه تنظیمات اولیه نشده است", Value = new { }, Error = new { } });
                }

                FixedBasicSetting FixedBasicSettingCreated = _service.FixedBasicSetting.GetById(editBasicSttingRequest.Id);

                if (FixedBasicSettingCreated == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "تنظیمات اولیه با این شناسه وجود ندارد", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(editBasicSttingRequest.FavoriteIcon))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "ایکون موردعلاقه فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (editBasicSttingRequest.FavoriteIcon == FixedBasicSettingCreated.FavoriteIcon)
                {
                    FixedBasicSettingCreated.FavoriteIcon = FixedBasicSettingCreated.FavoriteIcon;
                }
                if (string.IsNullOrEmpty(editBasicSttingRequest.Logo))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "لوگو فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (editBasicSttingRequest.Logo == FixedBasicSettingCreated.Logo)
                {
                    FixedBasicSettingCreated.Logo = FixedBasicSettingCreated.Logo;
                }
                if (string.IsNullOrEmpty(editBasicSttingRequest.FooterLogo))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "فوتر-لوگو فرستاده نشده است",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (editBasicSttingRequest.FooterLogo == FixedBasicSettingCreated.FooterLogo)
                {
                    FixedBasicSettingCreated.FooterLogo = FixedBasicSettingCreated.FooterLogo;
                }
                if (editBasicSttingRequest.FavoriteIcon != FixedBasicSettingCreated.FavoriteIcon)
                {
                    var res = _service.Picture.GetByAddress(FixedBasicSettingCreated.FavoriteIcon);

                    if (res != null)
                    {
                        var imageName = res.Address.Split("/")[res.Address.Split("/").Count() - 1];
                        if (System.IO.File.Exists(_appSettings.SaveImagePath + "\\FixedBasicSettings\\" + imageName))
                            System.IO.File.Delete(_appSettings.SaveImagePath + "\\FixedBasicSettings\\" + imageName);
                        _service.Picture.DeleteById(res.Id);
                    }
                    var favoriteIcon = _service.Picture.Upload(DateTime.Now.ToString("MMddHHmmss") + "-" + editBasicSttingRequest.FavoriteIconName, editBasicSttingRequest.FavoriteIcon, false, 3);
                    FixedBasicSettingCreated.FavoriteIcon = favoriteIcon.Address;
                }
                if (editBasicSttingRequest.Logo != FixedBasicSettingCreated.Logo)
                {
                    var res = _service.Picture.GetByAddress(FixedBasicSettingCreated.Logo);

                    if (res != null)
                    {
                        var imageName = res.Address.Split("/")[res.Address.Split("/").Count() - 1];
                        if (System.IO.File.Exists(_appSettings.SaveImagePath + "\\FixedBasicSettings\\" + imageName))
                            System.IO.File.Delete(_appSettings.SaveImagePath + "\\FixedBasicSettings\\" + imageName);
                        _service.Picture.DeleteById(res.Id);
                    }
                    var logo = _service.Picture.Upload(DateTime.Now.ToString("MMddHHmmss") + "-" + editBasicSttingRequest.LogoName, editBasicSttingRequest.Logo, false, 3);
                    FixedBasicSettingCreated.Logo = logo.Address;
                }
                if (editBasicSttingRequest.FooterLogo != FixedBasicSettingCreated.FooterLogo)
                {
                    var res = _service.Picture.GetByAddress(FixedBasicSettingCreated.FooterLogo);
                    if (res != null)
                    {
                        var imageName = res.Address.Split("/")[res.Address.Split("/").Count() - 1];
                        if (System.IO.File.Exists(_appSettings.SaveImagePath + "\\FixedBasicSettings\\" + imageName))
                            System.IO.File.Delete(_appSettings.SaveImagePath + "\\FixedBasicSettings\\" + imageName);
                        _service.Picture.DeleteById(res.Id);
                    }
                    var footerLogo = _service.Picture.Upload(DateTime.Now.ToString("MMddHHmmss") + "-" + editBasicSttingRequest.FooterLogoName, editBasicSttingRequest.FooterLogo, false, 3);
                    FixedBasicSettingCreated.FooterLogo = footerLogo.Address;
                }
                if (string.IsNullOrEmpty(editBasicSttingRequest.FirstColor))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه رنگ اول فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(editBasicSttingRequest.SecondColor))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه رنگ دوم فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(editBasicSttingRequest.ThirdColor))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه رنگ سوم فرستاده نشده است", Value = new { }, Error = new { } });
                }
                if (string.IsNullOrEmpty(editBasicSttingRequest.Email))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "ایمیل فرستاده نشده است", Value = new { }, Error = new { } });
                }

                FixedBasicSettingCreated.FirstColor = editBasicSttingRequest.FirstColor;
                FixedBasicSettingCreated.SecondColor = editBasicSttingRequest.SecondColor;
                FixedBasicSettingCreated.ThirdColor = editBasicSttingRequest.ThirdColor;
                FixedBasicSettingCreated.Email = editBasicSttingRequest.Email.Trim();
                FixedBasicSettingCreated.TelegramAddress = editBasicSttingRequest.TelegramAddress.Trim();
                FixedBasicSettingCreated.WhatsAppAddress = editBasicSttingRequest.WhatsAppAddress.Trim();
                FixedBasicSettingCreated.InstagramAddress = editBasicSttingRequest.InstagramAddress.Trim();
                FixedBasicSettingCreated.LinkedinAddress = editBasicSttingRequest.LinkedinAddress.Trim();

                _service.FixedBasicSetting.Edit(FixedBasicSettingCreated);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "تنظیمات اولیه ثابت با موفقیت به روز رسانی شد", Value = new { }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] DeleteBasicSttingRequest BasicStting)
        {
            try
            {
                if (BasicStting.Id == 0 || BasicStting.Id == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه تنظیمات اولیه نادرستی فرستاده شده است", Value = new { }, Error = new { } });
                }
                else
                {
                    BasicSetting basicSettingCreated = _service.BasicSetting.GetById(BasicStting.Id);
                    if (basicSettingCreated == null)
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "تنظیمات اولیه با این شناسه وجود ندارد", Value = new { }, Error = new { } });
                    }
                    basicSettingCreated.IsDelete = true;
                    _service.BasicSetting.Edit(basicSettingCreated);
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "تنظیمات اولیه با موفقیت حذف شد", Value = new { }, Error = new { } });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("DeleteFixedSetting")]
        public IActionResult DeleteFixedSetting([FromBody] DeleteFixedBasicSttingRequest BasicStting)
        {
            try
            {
                if (BasicStting.Id == 0 || BasicStting.Id == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه تنظیمات اولیه نادرستی فرستاده شده است", Value = new { }, Error = new { } });
                }
                else
                {
                    FixedBasicSetting fixedBasicSettingCreated = _service.FixedBasicSetting.GetById(BasicStting.Id);
                    if (fixedBasicSettingCreated == null)
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "تنظیمات اولیه ثابتی با این شناسه وجود ندارد", Value = new { }, Error = new { } });
                    }
                    fixedBasicSettingCreated.IsDelete = true;
                    _service.FixedBasicSetting.Edit(fixedBasicSettingCreated);
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "تنظیمات اولیه ثابت با موفقیت حذف شد", Value = new { }, Error = new { } });
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
                List<ShowBasicSttings> res = _service.BasicSetting.GetAll(languageId);
                ShowFixedBasicSttings fix = _service.FixedBasicSetting.GetAll();
                List<KeyValuePair<string, List<ShowBasicSttings>>> keyValuePair = new List<KeyValuePair<string, List<ShowBasicSttings>>>();
                var languages = _service.Language.GetLanguages();
                foreach (var item in languages)
                {
                    keyValuePair.Add(new KeyValuePair<string, List<ShowBasicSttings>>(item.Code, res.Where(w => w.LanguageId == item.Id).ToList()));
                }
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "تنظیمات اولیه با موفقیت ارسال شد",
                    Value = new { response = keyValuePair.ToDictionary(kvp => kvp.Key, kvp => kvp.Value), Fix = fix },
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
                BOShowBasicSttings res = _service.BasicSetting.BOGetAll(languageId);
                ShowFixedBasicSttings Fix = _service.FixedBasicSetting.GetAll();
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "تنظیمات اولیه ها با موفقیت ارسال شد", Value = new { response = res, Fix }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
    }
}
