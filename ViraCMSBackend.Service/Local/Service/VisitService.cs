using Microsoft.Extensions.Options;
using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Domain.Models;
using ViraCMSBackend.Service.Local.Interface;

namespace ViraCMSBackend.Service.Local.Service
{
    public class VisitService : IVisitService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;

        public VisitService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }

        public void Add(Visit visit)
        {
            _repository.Visit.Add(visit);   
        }
    }
}
