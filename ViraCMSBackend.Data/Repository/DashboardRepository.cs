using ViraCMSBackend.Core.Helpers;
using ViraCMSBackend.Data.Interface;
using ViraCMSBackend.Domain.DTOs;
using ViraCMSBackend.Domain.Models;
using static ViraCMSBackend.Domain.DTOs.BlogDTO;
using static ViraCMSBackend.Domain.DTOs.DashboardDTO;

namespace ViraCMSBackend.Data.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        ViraCMS_DBContext _repositoryContext;
        public DashboardRepository(ViraCMS_DBContext RepositoryContext)
        {
            _repositoryContext = RepositoryContext;
        }

        public List<MostCountryVisits> GetMostCountryVisits()
        {
            List<MostCountryVisits> res = new List<MostCountryVisits>();
            return _repositoryContext.Visits.Where(w => w.CreationDateTime >= DateTime.Now.AddDays(-30)).GroupBy(w => w.Country).Select(r => new MostCountryVisits
            {
                Country = r.Key,
                ViewCounter = r.Count(),
            }).OrderByDescending(w => w.ViewCounter).ToList();

        }

        public List<MostViewedBlogs> GetMostViwedBlogs(int langId)
        {
            List<MostViewedBlogs> res = new List<MostViewedBlogs>();
            foreach (var r in _repositoryContext.Blogs.Where(w => w.IsDelete == false &&
            w.IsActive == true &&
            (langId != 0 ? w.LanguageId == langId : true)
            ).OrderByDescending(w => w.ViewCounter)
            .ToList())
            {
                if (_repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive == true)
                {
                    res.Add(new MostViewedBlogs
                    {
                        Id = r.Id,
                        LanguageId = r.LanguageId,
                        LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive != false ? _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title : _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title + "(غیرفعال)",
                        Title = r.Title,
                        ViewCounter = r.ViewCounter,
                    });
                }
            }
            return res;
        }

        public List<MostViewedNews> GetMostViwedNews(int langId)
        {
            List<MostViewedNews> res = new List<MostViewedNews>();
            foreach (var r in _repositoryContext.News.Where(w => w.IsDelete == false &&
            w.IsActive == true &&
            (langId != 0 ? w.LanguageId == langId : true)
            ).OrderByDescending(w => w.ViewCounter)
            .ToList())
            {
                if (_repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive == true)
                {
                    res.Add(new MostViewedNews
                    {
                        Id = r.Id,
                        LanguageId = r.LanguageId,
                        LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive != false ? _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title : _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title + "(غیرفعال)",
                        Title = r.Title,
                        ViewCounter = r.ViewCounter,
                    });
                }
            }
            return res;
        }

        public List<MostViewedPages> GetMostViwedPages(int langId)
        {
            List<MostViewedPages> res = new List<MostViewedPages>();
            foreach (var r in _repositoryContext.Pages.Where(w => w.IsDelete == false &&
            w.IsActive == true &&
            (langId != 0 ? w.LanguageId == langId : true)
            ).OrderByDescending(w => w.ViewCounter)
            .ToList())
            {
                if (_repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive == true)
                {
                    res.Add(new MostViewedPages
                    {
                        Id = r.Id,
                        LanguageId = r.LanguageId,
                        LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive != false ? _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title : _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title + "(غیرفعال)",
                        Name = r.Name,
                        ViewCounter = r.ViewCounter,
                    });
                }
            }
            return res;
        }
    }
}
