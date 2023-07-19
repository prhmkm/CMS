using Microsoft.Extensions.Options;
using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Domain.DTOs;
using ViraCMSBackend.Domain.Models;
using ViraCMSBackend.Service.Local.Interface;

namespace ViraCMSBackend.Service.Local.Service
{
    public class PartnerCompanyService : IPartnerCompanyService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;

        public PartnerCompanyService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }

        public void Add(PartnerCompany partnerCompany)
        {
            _repository.PartnerCompany.Add(partnerCompany);
        }

        public void Edit(PartnerCompany partnerCompany)
        {
            _repository.PartnerCompany.Edit(partnerCompany);    
        }

        public PartnerCompany GetById(int id)
        {
            return _repository.PartnerCompany.GetById(id);
        }

        public List<PartnerCompanyDTO.ShowPartnerCompanies> GetAll(int id)
        {
            return _repository.PartnerCompany.GetAll(id);
        }

        public List<PartnerCompanyDTO.BOShowPartnerCompanies> BOGetAll(int id)
        {
            return _repository.PartnerCompany.BOGetAll(id);
        }
    }
}
