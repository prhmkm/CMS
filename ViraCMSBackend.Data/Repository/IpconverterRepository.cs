using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Data.Interface;
using ViraCMSBackend.Domain.Models;
namespace ViraCMSBackend.Data.Repository
{
    public class IpconverterRepository : BaseRepository<Country>, IIpconverterRepository
    {
        ViraCMS_DBContext _repositoryContext;
        private readonly AppSettings _appSettings;
        public IpconverterRepository(ViraCMS_DBContext RepositoryContext) : base(RepositoryContext)
        {
            _repositoryContext = RepositoryContext;
        }

        public string CountryNameToCountryCode(string countryname, string defaultLang)
        {
            var country = _repositoryContext.Countries.FirstOrDefault(w => w.CountryName == countryname);
            if (country == null)
            {
                return defaultLang;
            }
            return _repositoryContext.Languages.FirstOrDefault(w => w.Code == country.CountryCode).Code;
        }
    }
}
