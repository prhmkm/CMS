using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Data.Interface;
using ViraCMSBackend.Domain.Models;
using static ViraCMSBackend.Domain.DTOs.PartnerCompanyDTO;

namespace ViraCMSBackend.Data.Repository
{
    public class PartnerCompanyRepository : BaseRepository<PartnerCompany>, IPartnerCompanyRepository
    {
        ViraCMS_DBContext _repositoryContext;
        public PartnerCompanyRepository(ViraCMS_DBContext RepositoryContext) : base(RepositoryContext)
        {
            _repositoryContext = RepositoryContext;
        }

        public void Add(PartnerCompany partnerCompany)
        {
            Create(partnerCompany);
            Save();
        }

        public List<BOShowPartnerCompanies> BOGetAll(int id)
        {
            return _repositoryContext.PartnerCompanies.Where(w => w.IsDelete == false &&
            id != 0 ? w.LanguageId == id : true).Select(r => new BOShowPartnerCompanies
            {
                Id = r.Id,
                LanguageId = r.LanguageId,
                LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive != false ? _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title : _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title + "(غیرفعال)",
                Name = r.Name,
                ThumbPhoto = r.ThumbPhoto,
                HLink = r.HLink,
                IsActive = r.IsActive.GetValueOrDefault()
            }).ToList();
        }

        public void Edit(PartnerCompany partnerCompany)
        {
            Update(partnerCompany);
            Save();
        }

        public List<ShowPartnerCompanies> GetAll(int id)
        {
            return _repositoryContext.PartnerCompanies.Where(w=>w.IsDelete == false &&
            w.IsActive == true &&
            id != 0 ? w.LanguageId == id : true).Select(r => new ShowPartnerCompanies
            {
                Id = r.Id,
                LanguageId = r.LanguageId,
                LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive != false ? _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title : _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title + "(غیرفعال)",
                Name = r.Name,
                ThumbPhoto = r.ThumbPhoto,
                HLink = r.HLink,
            }).ToList();
        }

        public PartnerCompany GetById(int id)
        {
            return FindByCondition(w=>w.IsDelete == false && w.Id == id).FirstOrDefault();
        }
    }
}
