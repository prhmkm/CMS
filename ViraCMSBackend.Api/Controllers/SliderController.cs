using ViraCMSBackend.Core.Helpers;
using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Domain.Models;
using ViraCMSBackend.Service.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using static ViraCMSBackend.Domain.DTOs.SliderDTO;
using static ViraCMSBackend.Domain.DTOs.MenuDTO;

namespace ViraCMSBackend.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class SliderController : Controller
    {
        IServiceWrapper _service;
        private readonly AppSettings _appSettings;
        public SliderController(IServiceWrapper service, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _service = service;
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody] AddSliderRequest addSliderRequest)
        {
            try
            {
                if (!string.IsNullOrEmpty(addSliderRequest.SlideImage))
                {
                    if (addSliderRequest.LanguageId != null)
                    {
                        if (!string.IsNullOrEmpty(addSliderRequest.URL) && addSliderRequest.PageId != null)
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
                        var slideImage = _service.Picture.Upload(DateTime.Now.ToString("MMddHHmmss") + "-" + addSliderRequest.SlideImageName, addSliderRequest.SlideImage, false, 1);
                        Slider SliderCreated = new Slider()
                        {
                            LanguageId = addSliderRequest.LanguageId,
                            SlideImage = slideImage.Address,
                            URL = addSliderRequest.URL,
                            PageId = addSliderRequest.PageId,
                            IsDefault = addSliderRequest.IsDefault.GetValueOrDefault()
                        };
                        _service.Slider.AddSlider(SliderCreated);
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "اسلایدر با موفقیت ثبت شد", Value = new { }, Error = new { } });
                    }
                    else
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = " شناسه زبان فرستاده نشده است", Value = new { }, Error = new { } });
                    }
                }
                else
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = " عکس فرستاده نشده است", Value = new { }, Error = new { } });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [AllowAnonymous]
        [HttpGet("GetAllSliders")]
        public IActionResult GetAllSliders([FromHeader] string? language)
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
                List<ShowSliders> res = _service.Slider.GetAllSliders(languageId);
                List<ShowSliders> def = _service.Slider.GetAllDefaultSliders();
                List<KeyValuePair<string, List<ShowSliders>>> keyValuePair = new List<KeyValuePair<string, List<ShowSliders>>>();
                var languages = _service.Language.GetLanguages();
                foreach (var item in languages)
                {
                    keyValuePair.Add(new KeyValuePair<string, List<ShowSliders>>(item.Code, res.Where(w => w.LanguageId == item.Id).ToList()));
                }
                keyValuePair.Add(new KeyValuePair<string, List<ShowSliders>>("Default", def));
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "اسلاید ها با موفقیت ارسال شد", Value = new { response = keyValuePair.ToDictionary(kvp => kvp.Key, kvp => kvp.Value) }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpGet("BOGetAllSliders")]
        public IActionResult BOGetAllSliders([FromHeader] string? language)
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
                List<BOShowSliders> res = _service.Slider.BOGetAllSliders(languageId);
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "اسلاید ها با موفقیت ارسال شد", Value = new { response = res }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("Edit")]
        public IActionResult Edit([FromBody] UpdateSliderRequest slider)
        {
            try
            {
                if (slider.Id == 0 || slider.Id == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه اسلاید اشتباهی فرستاده شده است", Value = new { }, Error = new { } });
                }
                else
                {
                    Slider SliderCreated = _service.Slider.GetSliderById(slider.Id);
                    if (SliderCreated == null)
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "اسلایدری با این شناسه وجود ندارد", Value = new { }, Error = new { } });
                    }
                    if (string.IsNullOrEmpty(slider.SlideImage))
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "عکس اسلایدر فرستاده نشده است", Value = new { }, Error = new { } });
                    }
                    if (slider.SlideImage == SliderCreated.SlideImage)
                    {
                        SliderCreated.SlideImage = SliderCreated.SlideImage;
                    }
                    if (slider.SlideImage != SliderCreated.SlideImage)
                    {
                        var res = _service.Picture.GetByAddress(SliderCreated.SlideImage);
                        if (res != null)
                        {
                            var imageName = res.Address.Split("/")[res.Address.Split("/").Count() - 1];
                            if (System.IO.File.Exists(_appSettings.SaveImagePath + "\\Sliders\\" + imageName))
                                System.IO.File.Delete(_appSettings.SaveImagePath + "\\Sliders\\" + imageName);
                            _service.Picture.DeleteById(res.Id);
                        }
                        var slideImage = _service.Picture.Upload(DateTime.Now.ToString("MMddHHmmss") + "-" + slider.SlideImageName, slider.SlideImage, false, 1);
                        SliderCreated.SlideImage = slideImage.Address;
                    }
                    if (!string.IsNullOrEmpty(slider.URL) && slider.PageId != null)
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
                    if (slider.IsActive == null)
                    {
                        slider.IsActive = SliderCreated.IsActive;
                    }
                    if (string.IsNullOrEmpty(slider.URL))
                    {
                        slider.URL = SliderCreated.URL;
                    } 
                    if (slider.LanguageId == null)
                    {
                        slider.LanguageId = SliderCreated.LanguageId;
                    }
                    if (slider.IsDefault == null)
                    {
                        slider.IsDefault = SliderCreated.IsDefault;
                    }
                    SliderCreated.URL = slider.URL;
                    slider.PageId = slider.PageId;
                    SliderCreated.LanguageId = slider.LanguageId;
                    SliderCreated.IsDefault = slider.IsDefault.GetValueOrDefault();
                    SliderCreated.IsActive = slider.IsActive;
                    SliderCreated.CreationDateTime = SliderCreated.CreationDateTime;
                    _service.Slider.UpdateSlider(SliderCreated);
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "اسلایدر با موفقیت به روز رسانی شد", Value = new { }, Error = new { } });
                }

            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] DeleteSliderRequest slider)
        {
            try
            {
                if (slider.Id == 0 || slider.Id == null)
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه اسلاید اشتباهی فرستاده شده است", Value = new { }, Error = new { } });
                }
                else
                {
                    Slider SliderCreated = _service.Slider.GetSliderById(slider.Id);
                    if (SliderCreated == null)
                    {
                        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "اسلایدری با این شناسه وجود ندارد", Value = new { }, Error = new { } });
                    }
                    //if (SliderCreated.LanguageId == _service.Language.GetByCode(_appSettings.DefaultLanguage).Id)
                    //{
                    //    List<ShowSliders> res = _service.Slider.BOGetSliderByAlias(SliderCreated.Alias, 0);
                    //    //foreach (var item in res)
                    //    //{
                    //    //    item.IsDelete = true;
                    //    //    _service.Movie.UpdateMovie(item);
                    //    //}
                    //    if (res.Count > 1)
                    //    {
                    //        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "برای این اسلاید زبان های دیگری وچود دارد", Value = new { }, Error = new { } });
                    //    }
                    //}
                    SliderCreated.IsDelete = true;
                    _service.Slider.UpdateSlider(SliderCreated);
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "اسلایدر با موفقیت حذف شد", Value = new { }, Error = new { } });
                }

            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
        [HttpPost("GetSlideImage")]
        public IActionResult GetSlideImage([FromHeader] int id)
        {
            try
            {
                string res = _service.Slider.ShowSlideImage(id);
                if (string.IsNullOrEmpty(res))
                {
                    return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.BadRequest, Message = "شناسه اسلایدر اشتباهی فرستاده شده است", Value = new { }, Error = new { } });
                }
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.OK, Message = "عکس اسلایدر با موفقیت ارسال شد", Value = new { response = res }, Error = new { } });
            }
            catch (Exception ex)
            {
                return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
            }
        }
    }
}
