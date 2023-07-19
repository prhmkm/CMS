using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Data.Interface;
using ViraCMSBackend.Domain.Models;
using static ViraCMSBackend.Domain.DTOs.BasicSettingDTO;

namespace ViraCMSBackend.Data.Repository
{
    public class FixedBasicSettingRepository : BaseRepository<FixedBasicSetting>, IFixedBasicSettingRepository
    {
        ViraCMS_DBContext _repositoryContext;
        public FixedBasicSettingRepository(ViraCMS_DBContext RepositoryContext) : base(RepositoryContext)
        {
            _repositoryContext = RepositoryContext;
        }

        public void Add(FixedBasicSetting fixedBasicSetting)
        {
            if (FindAll().Count() == 0)
            {
                Create(fixedBasicSetting);
                Save();
            }
            else
            {
                FixedBasicSetting fixedSetting = GetById(0);
                fixedSetting.FavoriteIcon = fixedBasicSetting.FavoriteIcon;
                fixedSetting.Logo = fixedBasicSetting.Logo;
                fixedSetting.FooterLogo = fixedBasicSetting.FooterLogo;
                fixedSetting.FirstColor = fixedBasicSetting.FirstColor;
                fixedSetting.SecondColor = fixedBasicSetting.SecondColor;
                fixedSetting.ThirdColor = fixedBasicSetting.ThirdColor;
                fixedSetting.Email = fixedBasicSetting.Email;
                fixedSetting.TelegramAddress = fixedBasicSetting.TelegramAddress;
                fixedSetting.WhatsAppAddress = fixedBasicSetting.WhatsAppAddress;
                fixedSetting.InstagramAddress = fixedBasicSetting.InstagramAddress;
                fixedSetting.LinkedinAddress = fixedBasicSetting.LinkedinAddress;
                Update(fixedSetting);
                Save();
            }
        }

        public void Edit(FixedBasicSetting fixedBasicSetting)
        {
            Update(fixedBasicSetting);
            Save();
        }

        public ShowFixedBasicSttings GetAll()
        {
            return (from r in _repositoryContext.FixedBasicSettings
                    where r.IsDelete == false
                    select new ShowFixedBasicSttings
                    {
                        Id = r.Id,
                        FavoriteIcon = r.FavoriteIcon,
                        Logo = r.Logo,
                        FooterLogo = r.FooterLogo,
                        FirstColor = r.FirstColor,
                        SecondColor = r.SecondColor,
                        ThirdColor = r.ThirdColor,
                        Email = r.Email,
                        InstagramAddress = r.InstagramAddress,
                        TelegramAddress = r.TelegramAddress,
                        WhatsAppAddress = r.WhatsAppAddress,
                        LinkedinAddress = r.LinkedinAddress
                    }).FirstOrDefault();
        }

        public FixedBasicSetting GetById(int id)
        {
            if(id == 0)
            {
                return FindAll().FirstOrDefault();
            }
            else
            {
                return FindByCondition(w => w.Id == id).FirstOrDefault();
            }
        }
    }
}
