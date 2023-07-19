using ViraCMSBackend.Domain.Models;
using static ViraCMSBackend.Domain.DTOs.CounterDTO;

namespace ViraCMSBackend.Service.Local.Interface
{
    public interface ICounterService
    {
        void Add(Counter counter);
        void Edit(Counter counter);
        Counter GetById(int id);
        List<ShowCounters> GetAll(int id);
        List<BOShowCounters> BOGetAll(int id);
    }
}
