using ViraCMSBackend.Domain.Models;
using static ViraCMSBackend.Domain.DTOs.PictureDTO;

namespace ViraCMSBackend.Service.Local.Interface
{
    public interface IPictureService
    {
        PictureResponse FindById(long? id);
        UploadPic Upload(string objectId, string picture, bool thumbnail, int? id);
        void DeleteById(long id);
        List<Picture> FindByFolderId(long id);
        public Picture GetByAddress(string address);
    }
}
