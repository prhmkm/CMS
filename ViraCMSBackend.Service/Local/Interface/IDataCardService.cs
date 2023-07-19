using ViraCMSBackend.Domain.Models;
using static ViraCMSBackend.Domain.DTOs.DataCardDTO;

namespace ViraCMSBackend.Service.Local.Interface
{
    public interface IDataCardService
    {
        void Add(DataCard dataCard);
        void Edit(DataCard dataCard);
        DataCard GetById(int id);
        List<ShowDataCards> GetAll(int id);
        List<BOShowDataCards> BOGetAll(int id);
    }
}
