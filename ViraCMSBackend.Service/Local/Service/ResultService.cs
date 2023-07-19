using Microsoft.Extensions.Options;
using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Service.Local.Interface;
using static ViraCMSBackend.Domain.DTOs.ResultDTO;

namespace ViraCMSBackend.Service.Local.Service
{
    public class ResultService : IResultService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;

        public ResultService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }

        public List<ShowResult> results(string title, int pageSize, int id)
        {
            return _repository.Result.results(title, pageSize, id);
        }
    }
}
