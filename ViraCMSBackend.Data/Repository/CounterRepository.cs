using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Data.Interface;
using ViraCMSBackend.Domain.DTOs;
using ViraCMSBackend.Domain.Models;
using static ViraCMSBackend.Domain.DTOs.CounterDTO;

namespace ViraCMSBackend.Data.Repository
{
    public class CounterRepository : BaseRepository<Counter> , ICounterRepository
    {
        ViraCMS_DBContext _repositoryContext;
        public CounterRepository(ViraCMS_DBContext RepositoryContext) : base(RepositoryContext)
        {
            _repositoryContext = RepositoryContext;
        }

        public void Add(Counter counter)
        {
            Create(counter);
            Save();
        }

        public List<BOShowCounters> BOGetAll(int id)
        {
            List<BOShowCounters> res = new List<BOShowCounters>();
            foreach (var r in _repositoryContext.Counters.Where(w => w.IsDelete == false &&
            (id != 0 ? w.LanguageId == id : true)
            ).ToList())
            {
                if (_repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive == true)
                {
                    res.Add(new BOShowCounters
                    {
                        Id = r.Id,
                        LanguageId = r.LanguageId,
                        LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive != false ? _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title : _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title + "(غیرفعال)",
                        Title = r.Title,
                        Number = r.Number,
                        Icon = r.Icon,
                        IsActive = r.IsActive.GetValueOrDefault()
                    });
                }
            }
            return res;
        }

        public void Edit(Counter counter)
        {
            Update(counter);
            Save();
        }

        public List<ShowCounters> GetAll(int id)
        {
            List<ShowCounters> res = new List<ShowCounters>();
            foreach (var r in _repositoryContext.Counters.Where(w => w.IsDelete == false &&
            w.IsActive == true &&
            (id != 0 ? w.LanguageId == id : true)
            ).ToList())
            {
                if (_repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive == true)
                {
                    res.Add(new ShowCounters
                    {
                        Id = r.Id,
                        LanguageId = r.LanguageId,
                        LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive != false ? _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title : _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title + "(غیرفعال)",
                        Title = r.Title,
                        Number = r.Number,
                        Icon = r.Icon,
                    });
                }
            }
            return res;
        }

        public Counter GetById(int id)
        {
            return FindByCondition(w => w.Id == id && w.IsDelete == false).FirstOrDefault();
        }
    }
}
