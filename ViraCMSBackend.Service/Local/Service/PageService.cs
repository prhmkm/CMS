using Microsoft.Extensions.Options;
using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Domain.Models;
using ViraCMSBackend.Service.Local.Interface;
using static ViraCMSBackend.Domain.DTOs.PageDTO;

namespace ViraCMSBackend.Service.Local.Service
{
    public class PageService : IPageService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;

        public PageService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }

        public void Add(Page page)
        {
            _repository.Page.Add(page);
        }

        public void Edit(Page page)
        {
            _repository.Page.Edit(page);
        }

        public Page GetById(int Id)
        {
            return _repository.Page.GetById(Id);
        }

        public List<ShowPages> GetAll(int Id)
        {
            return _repository.Page.GetAll(Id);
        }

        public List<BOShowPages> BOGetAll(int Id)
        {
            return _repository.Page.BOGetAll(Id);
        }

        public Page GetByAlias(string alias)
        {
            return _repository.Page.GetByAlias(alias);  
        }
    }
}
