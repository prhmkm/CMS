using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Data.Interface;
using ViraCMSBackend.Domain.Models;
using static ViraCMSBackend.Domain.DTOs.ContentCardDTO;

namespace ViraCMSBackend.Data.Repository
{
    public class ContentCardRepository : BaseRepository<ContentCard>, IContentCardRepository
    {
        ViraCMS_DBContext _repositoryContext;
        public ContentCardRepository(ViraCMS_DBContext RepositoryContext) : base(RepositoryContext)
        {
            _repositoryContext = RepositoryContext;
        }

        public void Add(ContentCard contentCard)
        {
            Create(contentCard);
            Save();
        }

        public List<BOShowContentCards> BOGetAll(int id)
        {
            List<BOShowContentCards> res = new List<BOShowContentCards>();
            foreach (var r in _repositoryContext.ContentCards.Where(w => w.IsDelete == false &&
            (id != 0 ? w.LanguageId == id : true)
            ).ToList())
            {
                if (_repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive == true)
                {
                    res.Add(new BOShowContentCards
                    {
                        Id = r.Id,
                        LanguageId = r.LanguageId,
                        LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive != false ? _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title : _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title + "(غیرفعال)",
                        Title = r.Title,
                        Image = r.Image,
                        Description = r.Description,
                        IsActive = r.IsActive.GetValueOrDefault()
                    });
                }
            }
            return res;
        }

        public void Edit(ContentCard contentCard)
        {
            Update(contentCard);
            Save();
        }

        public List<ShowContentCards> GetAll(int id)
        {
            List<ShowContentCards> res = new List<ShowContentCards>();
            foreach (var r in _repositoryContext.ContentCards.Where(w => w.IsDelete == false &&
            w.IsActive == true &&
            (id != 0 ? w.LanguageId == id : true)
            ).ToList())
            {
                if (_repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive == true)
                {
                    res.Add(new ShowContentCards
                    {
                        Id = r.Id,
                        LanguageId = r.LanguageId,
                        LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive != false ? _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title : _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title + "(غیرفعال)",
                        Title = r.Title,
                        Image = r.Image,
                        Description = r.Description,
                    });
                }
            }
            return res;
        }

        public ContentCard GetById(int id)
        {
            return FindByCondition(w => w.Id == id && w.IsDelete == false).FirstOrDefault();
        }
    }
}
