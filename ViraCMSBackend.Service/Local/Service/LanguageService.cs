using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Domain.Models;
using ViraCMSBackend.Service.Local.Interface;
using Microsoft.Extensions.Options;

namespace ViraCMSBackend.Service.Local.Service
{
    public class LanguageService : ILanguageService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;

        public LanguageService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }
        public Language GetById(int id)
        {
            return _repository.Language.GetById(id);
        }

        public Language GetByCode(string code)
        {
            return _repository.Language.GetByCode(code);
        }

        public List<Language> GetLanguages()
        {
            return _repository.Language.GetLanguages();
        }

        public void Add(Language language)
        {
            _repository.Language.Add(language); 
        }

        public void Edit(Language language)
        {
            _repository.Language.Edit(language);
        }

        public List<Language> BOGetLanguages()
        {
            return _repository.Language.BOGetLanguages();
        }
    }
}
