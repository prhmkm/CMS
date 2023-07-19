

using Microsoft.Extensions.Options;
using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Domain.Models;
using ViraCMSBackend.Service.Local.Interface;
using static ViraCMSBackend.Domain.DTOs.NewsDTO;

namespace ViraCMSBackend.Service.Local.Service
{
    public class NewsService : INewsService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;

        public NewsService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }

        public void Add(News news)
        {
            _repository.News.Add(news);
        }

        public void Edit(News news)
        {
            _repository.News.Edit(news);
        }

        public News GetById(int id)
        {
            return _repository.News.GetById(id);
        }

        public List<ShowNews> GetAll(int id)
        {
            return _repository.News.GetAll(id);
        }

        public List<BOShowNews> BOGetAll(int id)
        {
            return _repository.News.BOGetAll(id);
        }

        public News GetByAlias(string alias)
        {
            return _repository.News.GetByAlias(alias);
        }
    }
}
