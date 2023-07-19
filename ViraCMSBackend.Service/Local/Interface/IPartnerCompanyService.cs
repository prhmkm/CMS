using static ViraCMSBackend.Domain.DTOs.PartnerCompanyDTO;
using ViraCMSBackend.Domain.Models;

namespace ViraCMSBackend.Service.Local.Interface
{
    public interface IPartnerCompanyService
    {
        void Add(PartnerCompany partnerCompany);
        void Edit(PartnerCompany partnerCompany);
        PartnerCompany GetById(int id);
        List<ShowPartnerCompanies> GetAll(int id);
        List<BOShowPartnerCompanies> BOGetAll(int id);
    }
}
