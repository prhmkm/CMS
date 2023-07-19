using ViraCMSBackend.Domain.Models;
using static ViraCMSBackend.Domain.DTOs.BasicSettingDTO;

namespace ViraCMSBackend.Data.Interface
{
    public interface IFixedBasicSettingRepository
    {
        void Add(FixedBasicSetting fixedBasicSetting);
        void Edit(FixedBasicSetting fixedBasicSetting);
        FixedBasicSetting GetById(int id);
        ShowFixedBasicSttings GetAll();
    }
}
