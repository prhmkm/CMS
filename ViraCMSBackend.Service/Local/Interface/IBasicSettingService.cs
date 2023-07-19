using ViraCMSBackend.Domain.Models;
using static ViraCMSBackend.Domain.DTOs.BasicSettingDTO;

namespace ViraCMSBackend.Service.Local.Interface
{
    public interface IBasicSettingService
    {
        void Add(BasicSetting basicSetting);
        void Edit(BasicSetting basicSetting);
        public BasicSetting GetById(int id);
        List<ShowBasicSttings> GetAll(int languageId);
        BOShowBasicSttings BOGetAll(int languageId);
    }
}
