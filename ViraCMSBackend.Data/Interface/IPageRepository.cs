using ViraCMSBackend.Domain.Models;
using static ViraCMSBackend.Domain.DTOs.PageDTO;

namespace ViraCMSBackend.Data.Interface
{
    public interface IPageRepository
    {
        void Add(Page page);
        void Edit(Page page);
        Page GetById(int Id);
        Page GetByAlias(string alias);
        List<ShowPages> GetAll(int Id);
        List<BOShowPages> BOGetAll(int Id);
    }
}
