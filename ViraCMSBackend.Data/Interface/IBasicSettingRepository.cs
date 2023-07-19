using ViraCMSBackend.Domain.Models;
using static ViraCMSBackend.Domain.DTOs.BasicSettingDTO;

namespace ViraCMSBackend.Data.Interface
{
    public interface IBasicSettingRepository
    {
        void Add(BasicSetting basicSetting);
        void Edit(BasicSetting basicSetting);
        BasicSetting GetById(int id);
        List<ShowBasicSttings> GetAll(int languageId);
        BOShowBasicSttings BOGetAll(int languageId);
    }
}
