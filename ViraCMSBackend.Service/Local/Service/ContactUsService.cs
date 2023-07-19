using Microsoft.Extensions.Options;
using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Domain.Models;
using ViraCMSBackend.Service.Local.Interface;
using static ViraCMSBackend.Domain.DTOs.ContactUsDTO;

namespace ViraCMSBackend.Service.Local.Service
{
    public class ContactUsService : IContactUsService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;

        public ContactUsService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }

        public void Add(ContactU contactUs)
        {
            _repository.ContactUs.Add(contactUs);   
        }

        public ContactU GetById(int id)
        {
            return _repository.ContactUs.GetById(id);
        }

        public List<ShowMessages> BOGetAll(int id, int type)
        {
            return _repository.ContactUs.BOGetAll(id, type);
        }

        public void EditReadSate(ContactU contactUs)
        {
            _repository.ContactUs.EditReadSate(contactUs);
        }
    }
}
