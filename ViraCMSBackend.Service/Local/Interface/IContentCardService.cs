using ViraCMSBackend.Domain.Models;
using static ViraCMSBackend.Domain.DTOs.ContentCardDTO;

namespace ViraCMSBackend.Service.Local.Interface
{
    public interface IContentCardService
    {
        void Add(ContentCard contentCard);
        void Edit(ContentCard contentCard);
        ContentCard GetById(int id);
        List<ShowContentCards> GetAll(int id);
        List<BOShowContentCards> BOGetAll(int id);
    }
}
