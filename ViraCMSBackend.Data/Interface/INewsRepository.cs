using ViraCMSBackend.Domain.Models;
using static ViraCMSBackend.Domain.DTOs.NewsDTO;

namespace ViraCMSBackend.Data.Interface
{
    public interface INewsRepository
    {
        void Add(News news);
        void Edit(News news);
        News GetById(int id);
        News GetByAlias(string alias);
        List<ShowNews> GetAll(int id);
        List<BOShowNews> BOGetAll(int id);
    }
}
