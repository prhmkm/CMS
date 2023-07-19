using ViraCMSBackend.Core.Helpers;
using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Data.Interface;
using ViraCMSBackend.Domain.Models;
using static ViraCMSBackend.Domain.DTOs.BlogDTO;

namespace ViraCMSBackend.Data.Repository
{
    public class BlogRepository : BaseRepository<Blog>, IBlogRepository
    {
        ViraCMS_DBContext _repositoryContext;
        public BlogRepository(ViraCMS_DBContext RepositoryContext) : base(RepositoryContext)
        {
            _repositoryContext = RepositoryContext;
        }

        public void Add(Blog blog)
        {
            Create(blog);
            Save();
        }

        public List<BOShowBlogs> BOGetAll(int id)
        {
            List<BOShowBlogs> res = new List<BOShowBlogs>();
            foreach (var r in _repositoryContext.Blogs.Where(w => w.IsDelete == false &&
            (id != 0 ? w.LanguageId == id : true)

            ).ToList())
            {
                if (_repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive == true)
                {
                    res.Add(new BOShowBlogs
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
                    }); ;
                }
            }
            return res;
        }

        public void Edit(Blog blog)
        {
            Update(blog);
            Save();
        }

        public List<ShowBlogs> GetAll(int id)
        {
            List<ShowBlogs> res = new List<ShowBlogs>();
            foreach (var r in _repositoryContext.Blogs.Where(w => w.IsDelete == false &&
            w.IsActive == true &&
            (id != 0 ? w.LanguageId == id : true)
            ).ToList())
            {
                if (_repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive == true)
                {
                    res.Add(new ShowBlogs
                    {
                        Id = r.Id,
                        LanguageId = r.LanguageId,
                        LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive != false ? _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title : _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title + "(غیرفعال)",
                        //LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title,
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

        public Blog GetByAlias(string alias)
        {
            return FindByCondition(w => w.Alias == alias && w.IsDelete == false).FirstOrDefault();
        }

        public Blog GetById(int id)
        {
            return FindByCondition(w => w.Id == id && w.IsDelete == false).FirstOrDefault();
        }
    }
}
