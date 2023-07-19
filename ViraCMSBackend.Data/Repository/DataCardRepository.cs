using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Data.Interface;
using ViraCMSBackend.Domain.DTOs;
using ViraCMSBackend.Domain.Models;
using static ViraCMSBackend.Domain.DTOs.DataCardDTO;

namespace ViraCMSBackend.Data.Repository
{
    public class DataCardRepository : BaseRepository<DataCard> , IDataCardRepository
    {
        ViraCMS_DBContext _repositoryContext;
        public DataCardRepository(ViraCMS_DBContext RepositoryContext) : base(RepositoryContext)
        {
            _repositoryContext = RepositoryContext;
        }

        public void Add(DataCard dataCard)
        {
            Create(dataCard);
            Save();
        }

        public List<BOShowDataCards> BOGetAll(int id)
        {
            List<BOShowDataCards> res = new List<BOShowDataCards>();
            foreach (var r in _repositoryContext.DataCards.Where(w => w.IsDelete == false &&
            (id != 0 ? w.LanguageId == id : true)
            ).ToList())
            {
                if (_repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive == true)
                {
                    res.Add(new BOShowDataCards
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

        public void Edit(DataCard dataCard)
        {
            Update(dataCard);
            Save();
        }

        public List<DataCardDTO.ShowDataCards> GetAll(int id)
        {
            List<ShowDataCards> res = new List<ShowDataCards>();
            foreach (var r in _repositoryContext.DataCards.Where(w => w.IsDelete == false &&
            w.IsActive == true &&
            (id != 0 ? w.LanguageId == id : true)
            ).ToList())
            {
                if (_repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive == true)
                {
                    res.Add(new ShowDataCards
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

        public DataCard GetById(int id)
        {
            return FindByCondition(w => w.Id == id && w.IsDelete == false).FirstOrDefault();
        }
    }
}
