using Microsoft.Extensions.Options;
using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Domain.DTOs;
using ViraCMSBackend.Domain.Models;
using ViraCMSBackend.Service.Local.Interface;
using static ViraCMSBackend.Domain.DTOs.BasicSettingDTO;

namespace ViraCMSBackend.Service.Local.Service
{
    public class BasicSettingService : IBasicSettingService
    {
        IRepositoryWrapper _repository;
        private readonly AppSettings _appSettings;

        public BasicSettingService(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }

        public void Add(BasicSetting basicSetting)
        {
            _repository.BasicSetting.Add(basicSetting);
        }

        public void Edit(BasicSetting basicSetting)
        {
            _repository.BasicSetting.Edit(basicSetting);
        }

        public List<ShowBasicSttings> GetAll(int languageId)
        {
            return _repository.BasicSetting.GetAll(languageId);
        }

        public BOShowBasicSttings BOGetAll(int languageId)
        {
            return _repository.BasicSetting.BOGetAll(languageId);
        }

        public BasicSetting GetById(int id)
        {
            return _repository.BasicSetting.GetById(id);    
        }
    }
}
