using ViraCMSBackend.Domain.Models;
using static ViraCMSBackend.Domain.DTOs.BasicSettingDTO;

namespace ViraCMSBackend.Service.Local.Interface
{
    public interface IFixedBasicSettingService
    {
        void Add(FixedBasicSetting fixedBasicSetting);
        void Edit(FixedBasicSetting fixedBasicSetting);
        FixedBasicSetting GetById(int id);
        ShowFixedBasicSttings GetAll();
    }
}
