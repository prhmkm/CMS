using ViraCMSBackend.Domain.Models;
using static ViraCMSBackend.Domain.DTOs.PictureDTO;

namespace ViraCMSBackend.Data.Interface
{
    public interface IPictureRepository
    {
        PictureResponse FindById(long? id);
        long Add(Picture picture);
        void DeleteById(long id);
        List<Picture> FindByFolderId(long id);
        Picture GetByAddress(string address);
    }
}
