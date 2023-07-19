using ViraCMSBackend.Domain.Models;
using static ViraCMSBackend.Domain.DTOs.PartnerCompanyDTO;

namespace ViraCMSBackend.Data.Interface
{
    public interface IPartnerCompanyRepository
    {
        void Add(PartnerCompany partnerCompany);
        void Edit(PartnerCompany partnerCompany);
        PartnerCompany GetById(int id);
        List<ShowPartnerCompanies> GetAll(int id);
        List<BOShowPartnerCompanies> BOGetAll(int id);
    }
}
