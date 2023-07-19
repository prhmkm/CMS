using ViraCMSBackend.Domain.Models;

namespace ViraCMSBackend.Service.Local.Interface
{
    public interface ILanguageService
    {
        List<Language> GetLanguages();
        List<Language> BOGetLanguages();
        void Add(Language language);
        void Edit(Language language);
        Language GetById(int id);
        Language GetByCode(string code);
    }
}
