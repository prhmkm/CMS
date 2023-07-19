using ViraCMSBackend.Core.Helpers;
using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Domain.Models;
using ViraCMSBackend.Service.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using System.Security.Claims;
using static ViraCMSBackend.Domain.DTOs.PictureDTO;

namespace ViraCMSBackend.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PictureController : Controller
    {
        private readonly AppSettings _appSettings;
        private readonly IServiceWrapper _service;

        public PictureController(IServiceWrapper service, IOptions<AppSettings> appSettings)
        {
            _service = service;
            _appSettings = appSettings.Value;
        }
        [HttpPost("Upload")]
        public IActionResult Upload([FromBody] UploadRequest model)
        {
            try
            {
                if (model == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "مقادیر ورودی معتبر نمی‌باشند.",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (!ModelState.IsValid)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "مقادیر ورودی معتبر نمی‌باشند.",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(model.Picture))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "موارد الزامی را وارد نمایید.",
                        Value = new { },
                        Error = new { }
                    });
                }
                //if (HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value != "User")
                //{
                //    return Ok(new
                //    {
                //        TimeStamp = DateTime.Now,
                //        ResponseCode = HttpStatusCode.Unauthorized,
                //        Message = "دسترسی غیر مجاز.",
                //        Value = new { },
                //        Error = new { }
                //    });
                //}
                //if (model.FolderId == 0)
                //{
                //    model.FolderId = _service.Folder.GetRootFolderId();
                //}

                UploadPic uploadPic = _service.Picture.Upload(DateTime.Now.ToString("MMddHHmmss") + "-" + model.PictureName, model.Picture, model.Thumbnail, null);
                if (uploadPic.Id != 0 && uploadPic.Id != -1)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.OK,
                        Message = "عکس با موفقیت آپلود شد.",
                        Value = new { Response = uploadPic },
                        Error = new { }
                    });
                }
                else if (uploadPic.Id == -1)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.MethodNotAllowed,
                        Message = "فضای ذخیره سازی به حداکثر مجاز رسیده است .",
                        Value = new { },
                        Error = new { }
                    });
                }
                else
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.InternalServerError,
                        Message = "آپلود عکس انجام نشد.",
                        Value = new { },
                        Error = new { }
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.InternalServerError,
                    Message = "خطا داخلی سرور رخ داده است",
                    Value = new { },
                    Error = new { message = ex.Message }
                });
            }
        }
        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] DeleteRequest model)
        {
            try
            {
                if (model.Id <= 0)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "شناسه عکس الزامی است.",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value != "User")
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.Unauthorized,
                        Message = "دسترسی غیر مجاز.",
                        Value = new { },
                        Error = new { }
                    });
                }
                PictureResponse picture = _service.Picture.FindById(model.Id);
                List<string> path = picture.Address.Split("//").ToList();
                var count = path.Count();
                if (System.IO.File.Exists(_appSettings.SaveImagePath + path[count - 2] + "\\" + path[count - 1]))
                {
                    System.IO.File.Delete(_appSettings.SaveImagePath + path[count - 2] + "\\" + path[count - 1]);
                }
                path = picture.Thumbnail.Split("//").ToList();
                count = path.Count();
                if (System.IO.File.Exists(_appSettings.SaveImagePath + path[count - 2] + "\\" + path[count - 1]))
                {
                    System.IO.File.Delete(_appSettings.SaveImagePath + path[count - 2] + "\\" + path[count - 1]);
                }

                _service.Picture.DeleteById(picture.Id);
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "حذف عکس با موفقیت انجام شد.",
                    Value = new { },
                    Error = new { }
                });


            }
            catch (Exception)
            {
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.InternalServerError,
                    Message = "حذف عکس انجام نشد.",
                    Value = new { },
                    Error = new { }
                });
            }
        }
    }
}
