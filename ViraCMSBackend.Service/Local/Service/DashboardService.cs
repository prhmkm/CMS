using Microsoft.Extensions.Options;
using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Domain.DTOs;
using ViraCMSBackend.Service.Local.Interface;

namespace ViraCMSBackend.Service.Local.Service
{
    public class DashboardService : IDashboardService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;

        public DashboardService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }

        public List<DashboardDTO.MostViewedBlogs> GetMostViwedBlogs(int langId)
        {
            return _repository.Dashboard.GetMostViwedBlogs(langId);
        }

        public List<DashboardDTO.MostViewedNews> GetMostViwedNews(int langId)
        {
            return _repository.Dashboard.GetMostViwedNews(langId);

        }

        public List<DashboardDTO.MostViewedPages> GetMostViwedPages(int langId)
        {
            return _repository.Dashboard.GetMostViwedPages(langId);
        }

        public List<DashboardDTO.MostCountryVisits> GetMostCountryVisits()
        {
            return _repository.Dashboard.GetMostCountryVisits();
        }
    }
}
