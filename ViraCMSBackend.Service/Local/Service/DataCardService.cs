using Microsoft.Extensions.Options;
using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Domain.DTOs;
using ViraCMSBackend.Domain.Models;
using ViraCMSBackend.Service.Local.Interface;

namespace ViraCMSBackend.Service.Local.Service
{
    public class DataCardService : IDataCardService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;

        public DataCardService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }

        public void Add(DataCard dataCard)
        {
            _repository.DataCard.Add(dataCard);
        }

        public void Edit(DataCard dataCard)
        {
            _repository.DataCard.Edit(dataCard);
        }

        public DataCard GetById(int id)
        {
            return _repository.DataCard.GetById(id);    
        }

        public List<DataCardDTO.ShowDataCards> GetAll(int id)
        {
            return _repository.DataCard.GetAll(id);
        }

        public List<DataCardDTO.BOShowDataCards> BOGetAll(int id)
        {
            return _repository.DataCard.BOGetAll(id);
        }
    }
}
