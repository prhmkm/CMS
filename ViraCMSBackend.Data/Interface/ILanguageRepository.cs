using ViraCMSBackend.Domain.Models;
using static ViraCMSBackend.Domain.DTOs.LanguageDTO;

namespace ViraCMSBackend.Data.Interface
{
    public interface ILanguageRepository
    {
        List<Language> GetLanguages();
        List<Language> BOGetLanguages();
        void Add(Language language);
        void Edit(Language language);
        Language GetById(int id);
        Language GetByCode(string code);
    }
}
