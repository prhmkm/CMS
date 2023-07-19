using Microsoft.Extensions.Options;
using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Domain.DTOs;
using ViraCMSBackend.Domain.Models;
using ViraCMSBackend.Service.Local.Interface;

namespace ViraCMSBackend.Service.Local.Service
{
    public class CounterService : ICounterService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;

        public CounterService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }

        public void Add(Counter counter)
        {
            _repository.Counter.Add(counter);
        }

        public void Edit(Counter counter)
        {
            _repository.Counter.Edit(counter);
        }

        public Counter GetById(int id)
        {
            return _repository.Counter.GetById(id);
        }

        public List<CounterDTO.ShowCounters> GetAll(int id)
        {
            return _repository.Counter.GetAll(id);
        }

        public List<CounterDTO.BOShowCounters> BOGetAll(int id)
        {
            return _repository.Counter.BOGetAll(id);
        }
    }
}
