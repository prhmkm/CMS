using ViraCMSBackend.Core.Helpers;
using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Domain.Models;
using ViraCMSBackend.Service.Local.Interface;
using Microsoft.Extensions.Options;
using static ViraCMSBackend.Domain.DTOs.PictureDTO;

namespace ViraCMSBackend.Service.Local.Service
{
    public class PictureService : IPictureService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;

        public PictureService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }
        public void DeleteById(long id)
        {
            _repository.Picture.DeleteById(id);
        }

        public List<Picture> FindByFolderId(long id)
        {
            return _repository.Picture.FindByFolderId(id);
        }
        public PictureResponse FindById(long? id)
        {
            return _repository.Picture.FindById(id);
        }

        public UploadPic Upload(string objectId, string picture, bool thumbnail, int? id)
        {
            List<string> _imagName = objectId.Split(".").ToList();
            string imgName = null;
            for (int i = 0; i < _imagName.Count - 1; i++)
            {
                imgName = imgName + _imagName[i] + ".";
            }
            string path = Path.Combine(_appSettings.SaveImagePath + "\\");
            if (id == 1)
            {
                 path = Path.Combine(_appSettings.SaveImagePath + "\\Sliders\\");
            }
            if (id == 2)
            {
                 path = Path.Combine(_appSettings.SaveImagePath + "\\News\\");
            }
            if (id == 3)
            {
                 path = Path.Combine(_appSettings.SaveImagePath + "\\FixedBasicSettings\\");
            }
            if (id == 4)
            {
                 path = Path.Combine(_appSettings.SaveImagePath + "\\DataCards\\");
            }
            if (id == 5)
            {
                 path = Path.Combine(_appSettings.SaveImagePath + "\\Counters\\");
            }
            if (id == 6)
            {
                 path = Path.Combine(_appSettings.SaveImagePath + "\\ContentCards\\");
            }
            if (id == 7)
            {
                path = Path.Combine(_appSettings.SaveImagePath + "\\Blogs\\");
            }
            if (id == 8)
            {
                path = Path.Combine(_appSettings.SaveImagePath + "\\PartnerCompanies\\");
            }
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string Address = null;
            string thumbnailAddress = null;
            string imageName = null;
            string imageNameThumb = null;
            if (thumbnail == false)
            {
                imageName = Convertor.Base64ToImage(picture, path, objectId.Split(".")[0]);
                Address = _appSettings.PublishImagePath + "//" + imageName;
            }
            if (thumbnail == true)
            {
                imageNameThumb = Convertor.Base64ToThumbnail(picture, path, objectId.Split(".")[0] + "thumb" /*+ _imagName[_imagName.Count - 1]*/);
                thumbnailAddress = null;
            }
            if (thumbnail == false)
            {
                if (id == 1)
                {
                    Address = _appSettings.PublishImagePath + "//Sliders//" + imageName;
                }
                if (id == 2)
                {
                    Address = _appSettings.PublishImagePath + "//News//" + imageName;
                }
                if (id == 3)
                {
                    Address = _appSettings.PublishImagePath + "//FixedBasicSettings//" + imageName;
                }
                if (id == 4)
                {
                    Address = _appSettings.PublishImagePath + "//DataCards//" + imageName;
                }
                if (id == 5)
                {
                    Address = _appSettings.PublishImagePath + "//Counters//" + imageName;
                }
                if (id == 6)
                {
                    Address = _appSettings.PublishImagePath + "//ContentCards//" + imageName;
                }
                if (id == 7)
                {
                    Address = _appSettings.PublishImagePath + "//Blogs//" + imageName;
                }
                if (id == 8)
                {
                    Address = _appSettings.PublishImagePath + "//PartnerCompanies//" + imageName;
                }
            }
            if (thumbnail == true)
            {
                thumbnailAddress = _appSettings.PublishImagePath + "//" + imageNameThumb;
                if (id == 1)
                {
                    thumbnailAddress = _appSettings.PublishImagePath + "//Sliders//" + imageNameThumb;
                }
                if (id == 2)
                {
                    thumbnailAddress = _appSettings.PublishImagePath + "//News//" + imageNameThumb;
                }
                if (id == 3)
                {
                    thumbnailAddress = _appSettings.PublishImagePath + "//FixedBasicSettings//" + imageNameThumb;
                }
                if (id == 4)
                {
                    thumbnailAddress = _appSettings.PublishImagePath + "//DataCards//" + imageNameThumb;
                }
                if (id == 5)
                {
                    thumbnailAddress = _appSettings.PublishImagePath + "//Counters//" + imageNameThumb;
                }
                if (id == 6)
                {
                    thumbnailAddress = _appSettings.PublishImagePath + "//ContentCards//" + imageNameThumb;
                }
                if (id == 7)
                {
                    thumbnailAddress = _appSettings.PublishImagePath + "//Blogs//" + imageNameThumb;
                }
                if (id == 8)
                {
                    thumbnailAddress = _appSettings.PublishImagePath + "//PartnerCompanies//" + imageNameThumb;
                }
            }
            if (imageName == "crash" || imageNameThumb == "crash")
            {
                return new UploadPic { Address = null, Id = 0 };
            }
            return new UploadPic { Address = Address , ThumpAddress = thumbnailAddress ,  Id = _repository.Picture.Add(new Picture { Address = Address, ImageName = imageName, Thumbnail = thumbnailAddress }) };
        }

        public Picture GetByAddress(string address)
        {
            return _repository.Picture.GetByAddress(address);   
        }
    }
}
