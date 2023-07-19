

using Microsoft.Extensions.Options;
using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Domain.Models;
using ViraCMSBackend.Service.Local.Interface;
using static ViraCMSBackend.Domain.DTOs.ContentCardDTO;

namespace ViraCMSBackend.Service.Local.Service
{
    public class ContentCardService : IContentCardService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;

        public ContentCardService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }

        public void Add(ContentCard contentCard)
        {
            _repository.ContentCard.Add(contentCard);
        }

        public void Edit(ContentCard contentCard)
        {
            _repository.ContentCard.Edit(contentCard);
        }

        public ContentCard GetById(int id)
        {
            return _repository.ContentCard.GetById(id);
        }

        public List<ShowContentCards> GetAll(int id)
        {
            return _repository.ContentCard.GetAll(id);
        }

        public List<BOShowContentCards> BOGetAll(int id)
        {
            return _repository.ContentCard.BOGetAll(id);
        }
    }
}
