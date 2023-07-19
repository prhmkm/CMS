using ViraCMSBackend.Core.Helpers;
using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Data.Interface;
using ViraCMSBackend.Domain.Models;
using static ViraCMSBackend.Domain.DTOs.NewsDTO;

namespace ViraCMSBackend.Data.Repository
{
    public class NewsRepository : BaseRepository<News>, INewsRepository
    {
        ViraCMS_DBContext _repositoryContext;
        public NewsRepository(ViraCMS_DBContext RepositoryContext) : base(RepositoryContext)
        {
            _repositoryContext = RepositoryContext;
        }

        public void Add(News news)
        {
            Create(news);
            Save();
        }

        public List<BOShowNews> BOGetAll(int id)
        {
            List<BOShowNews> res = new List<BOShowNews>();
            foreach (var r in _repositoryContext.News.Where(w => w.IsDelete == false &&
            (id != 0 ? w.LanguageId == id : true)
            ).ToList())
            {
                if (_repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive == true)
                {
                    res.Add(new BOShowNews
                    {
                        Id = r.Id,
                        LanguageId = r.LanguageId,
                        LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive != false ? _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title : _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title + "(غیرفعال)",
                        Title = r.Title,
                        Alias = r.Alias,
                        DateTime = DateHelpers.ToPersianDate(r.CreationDateTime, true, "/").Substring(0, 16),
                        ShortText = r.ShortText,
                        ThumbPhoto = r.ThumbPhoto,
                        CustomHTML = r.CustomHTML,
                        ViewCounter = r.ViewCounter,
                        IsActive = r.IsActive.GetValueOrDefault()
                    });
                }
            }
            return res;
        }

        public void Edit(News news)
        {
            Update(news);
            Save();
        }

        public List<ShowNews> GetAll(int id)
        {
            List<ShowNews> res = new List<ShowNews>();
            foreach (var r in _repositoryContext.News.Where(w => w.IsDelete == false &&
            w.IsActive == true &&
            (id != 0 ? w.LanguageId == id : true)
            ).ToList())
            {
                if (_repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive == true)
                {
                    res.Add(new ShowNews
                    {
                        Id = r.Id,
                        LanguageId = r.LanguageId,
                        LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive != false ? _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title : _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title + "(غیرفعال)",
                        Title = r.Title,
                        Alias = r.Alias,
                        DateTime = DateHelpers.ToPersianDate(r.CreationDateTime, true, "/").Substring(0, 16),
                        ShortText = r.ShortText,
                        ThumbPhoto = r.ThumbPhoto,
                        CustomHTML = r.CustomHTML,
                        ViewCounter = r.ViewCounter,
                    });
                }
            }
            return res;
        }

        public News GetByAlias(string alias)
        {
            return FindByCondition(w => w.Alias == alias && w.IsDelete == false).FirstOrDefault();
        }

        public News GetById(int id)
        {
            return FindByCondition(w => w.Id == id && w.IsDelete == false).FirstOrDefault();
        }
    }
}

