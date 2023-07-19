using ViraCMSBackend.Domain.Models;
using static ViraCMSBackend.Domain.DTOs.ContactUsDTO;

namespace ViraCMSBackend.Data.Interface
{
    public interface IContactUsRepository
    {
        void Add(ContactU contactUs);
        void EditReadSate(ContactU contactUs);
        ContactU GetById(int id);
        List<ShowMessages> BOGetAll(int id, int type);
    }
}
