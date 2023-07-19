using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Data.Interface;
using ViraCMSBackend.Domain.Models;
using static ViraCMSBackend.Domain.DTOs.BasicSettingDTO;

namespace ViraCMSBackend.Data.Repository
{
    public class BasicSettingRepository : BaseRepository<BasicSetting>, IBasicSettingRepository
    {
        ViraCMS_DBContext _repositoryContext;
        public BasicSettingRepository(ViraCMS_DBContext RepositoryContext) : base(RepositoryContext)
        {
            _repositoryContext = RepositoryContext;
        }

        public void Add(BasicSetting basicSetting)
        {
            int count = FindByCondition(w => /*w.IsActive == true &&*/ w.IsDelete == false && w.LanguageId == basicSetting.LanguageId).Count();
            if (count == 0)
            {
                Create(basicSetting);
                Save();
            }
            else
            {
                BasicSetting BasicSettingCreated = FindByCondition(w=>w.IsDelete == false && w.LanguageId == basicSetting.LanguageId).FirstOrDefault();
                BasicSettingCreated.LanguageId = basicSetting.LanguageId;
                BasicSettingCreated.Title = basicSetting.Title;
                BasicSettingCreated.Address = basicSetting.Address;
                BasicSettingCreated.PhoneNumber = basicSetting.PhoneNumber;
                BasicSettingCreated.MobileNumber = basicSetting.MobileNumber;
                BasicSettingCreated.WorkingHours = basicSetting.WorkingHours;
                BasicSettingCreated.Fax = basicSetting.Fax;
                BasicSettingCreated.FooterText = basicSetting.FooterText;
                BasicSettingCreated.AboutUs = basicSetting.AboutUs;
                BasicSettingCreated.IsActive = basicSetting.IsActive;
                Update(basicSetting);
                Save();
            }
        }

        public BOShowBasicSttings BOGetAll(int languageId)
        {
            BOShowBasicSttings res = new BOShowBasicSttings();
            foreach (var r in _repositoryContext.BasicSettings.Where(w => w.IsDelete == false &&
            w.IsActive == true &&
            (languageId != 0 ? w.LanguageId == languageId : true)).ToList())
            {
                if (_repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive == true)
                {
                    res.Id = r.Id;
                    res.LanguageId = r.LanguageId;
                    res.LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title;
                    if (_repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive == false)
                    {
                        res.LanguageTitle = res.LanguageTitle + "(غیر فعال)";
                    }
                    res.Title = r.Title;
                    res.Address = r.Address;
                    res.PhoneNumber = r.PhoneNumber;
                    res.MobileNumber = r.MobileNumber;
                    res.WorkingHours = r.WorkingHours;
                    res.Fax = r.Fax;
                    res.FooterText = r.FooterText;
                    res.AboutUs = r.AboutUs;
                    res.IsActive = r.IsActive.GetValueOrDefault();
                }
            }
            return res;
        }

        public void Edit(BasicSetting basicSetting)
        {
            if (basicSetting.IsActive == true)
            {
                BasicSetting res = FindByCondition(w => w.IsActive == true && w.IsDelete == false && w.LanguageId == basicSetting.LanguageId).FirstOrDefault();
                res.IsActive = false;
                Update(res);
                Save();
            }
            Update(basicSetting);
            Save();
        }

        public List<ShowBasicSttings> GetAll(int languageId)
        {
            var res = new List<ShowBasicSttings>();
            foreach (var r in _repositoryContext.BasicSettings.Where(w => w.IsDelete == false &&
            w.IsActive == true &&
            (languageId != 0 ? w.LanguageId == languageId : true)).ToList())
            {
                if (_repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive == true)
                {
                    res.Add(new ShowBasicSttings
                    {
                        Id = r.Id,
                        LanguageId = r.LanguageId,
                        LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive != false ? _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title : _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title + "(غیرفعال)",
                        Title = r.Title,
                        Address = r.Address,
                        PhoneNumber = r.PhoneNumber,
                        MobileNumber = r.MobileNumber,
                        WorkingHours = r.WorkingHours,
                        Fax = r.Fax,
                        FooterText = r.FooterText,
                        AboutUs = r.AboutUs,
                    });
                }
            }
            return res;

        }

        public BasicSetting GetById(int id)
        {
            if (id == 0)
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
