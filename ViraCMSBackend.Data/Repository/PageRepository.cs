using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Data.Interface;
using ViraCMSBackend.Domain.DTOs;
using ViraCMSBackend.Domain.Models;
using static ViraCMSBackend.Domain.DTOs.PageDTO;

namespace ViraCMSBackend.Data.Repository
{
    public class PageRepository : BaseRepository<Page>, IPageRepository
    {
        ViraCMS_DBContext _repositoryContext;
        public PageRepository(ViraCMS_DBContext RepositoryContext) : base(RepositoryContext)
        {
            _repositoryContext = RepositoryContext;
        }

        public void Add(Page page)
        {
            Create(page);
            Save();
        }

        public List<BOShowPages> BOGetAll(int Id)
        {
            List<BOShowPages> res = new List<BOShowPages>();
            foreach (var r in _repositoryContext.Pages.Where(w => w.IsDelete == false &&
            (Id != 0 ? w.LanguageId == Id : true)
            ).OrderBy(w => w.LanguageId)
            .ToList())
            {
                if (_repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive == true)
                {
                    res.Add(new BOShowPages
                    {
                        Id = r.Id,
                        LanguageId = r.LanguageId,
                        LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive != false ? _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title : _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title + "(غیرفعال)",
                        Name = r.Name,
                        CustomHTML = r.CustomHTML,
                        ViewCounter = r.ViewCounter,
                        IsActive = r.IsActive.GetValueOrDefault()
                    });
                }
            }
            return res;
        }

        public void Edit(Page page)
        {
            Update(page);
            Save();
        }

        public List<ShowPages> GetAll(int Id)
        {
            List<ShowPages> res = new List<ShowPages>();
            foreach (var r in _repositoryContext.Pages.Where(w => w.IsDelete == false &&
            w.IsActive == true &&
            (Id != 0 ? w.LanguageId == Id : true)
            ).ToList())
            {
                if (_repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive == true)
                {
                    res.Add(new ShowPages
                    {
                        Id = r.Id,
                        LanguageId = r.LanguageId,
                        LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive != false ? _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title : _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title + "(غیرفعال)",
                        Name = r.Name,
                        CustomHTML = r.CustomHTML,
                        ViewCounter = r.ViewCounter,
                    });
                }
            }
            return res;
        }

        public Page GetByAlias(string alias)
        {
            return FindByCondition(w => w.Alias == alias && w.IsDelete == false).FirstOrDefault();
        }

        public Page GetById(int Id)
        {
            return FindByCondition(w => w.Id == Id && w.IsDelete == false).FirstOrDefault();
        }
    }
}
