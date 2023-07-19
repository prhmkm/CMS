using System.Reflection.Metadata;
using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Data.Interface;
using ViraCMSBackend.Domain.Models;

namespace ViraCMSBackend.Data.Repository
{
    public class LanguageRepository : BaseRepository<Language>, ILanguageRepository
    {
        ViraCMS_DBContext _repositoryContext;
        public LanguageRepository(ViraCMS_DBContext RepositoryContext) : base(RepositoryContext)
        {
            _repositoryContext = RepositoryContext;
        }

        public List<Language> GetLanguages()
        {
            return FindByCondition(w=>w.IsActive == true).ToList();
        }

        public Language GetById(int id)
        {
            return FindByCondition(w => w.Id == id).FirstOrDefault();
        }

        public Language GetByCode(string code)
        {
            return FindByCondition(w => w.Code == code).FirstOrDefault();
        }

        public void Add(Language language)
        {
            Create(language);
            Save();
        }

        public void Edit(Language language)
        {
            Update(language);
            Save();
        }

        public List<Language> BOGetLanguages()
        {
            return FindAll().ToList();
        }
    }
}
