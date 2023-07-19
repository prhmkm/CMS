using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Data.Interface;
using ViraCMSBackend.Domain.Models;
using static ViraCMSBackend.Domain.DTOs.BlogDTO;
using static ViraCMSBackend.Domain.DTOs.SliderDTO;

namespace ViraCMSBackend.Data.Repository
{
    public class SliderRepository : BaseRepository<Slider>, ISliderRepository
    {
        ViraCMS_DBContext _repositoryContext;
        public SliderRepository(ViraCMS_DBContext RepositoryContext) : base(RepositoryContext)
        {
            _repositoryContext = RepositoryContext;
        }

        public void AddSlider(Slider slider)
        {
            Create(slider);
            Save();
        }

        public List<BOShowSliders> BOGetAllSliders(int languageId)
        {
            List<BOShowSliders> res = new List<BOShowSliders>();
            foreach (var r in _repositoryContext.Sliders.Where(r => r.IsDelete == false &&
                    (languageId != 0 ? r.LanguageId == languageId : true))
                .ToList())
            {
                if (_repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive == true)
                {
                    res.Add(new BOShowSliders
                    {
                        Id = r.Id,
                        IsDefault = r.IsDefault,
                        LanguageId = r.LanguageId,
                        LanguageCode = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Code,
                        LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive != false ? _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title : _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title + "(غیرفعال)",
                        URL = r.URL,
                        PageId = r.PageId,
                        PageAlias = _repositoryContext.Pages.Where(w => w.Id == r.PageId).Select(w => w.Alias).FirstOrDefault(),
                        PageTitle = _repositoryContext.Pages.Where(w => w.Id == r.PageId).Select(w => w.Name).FirstOrDefault(),
                        IsActive = r.IsActive,
                        CreationDateTime = r.CreationDateTime
                    });
                }
            }
            return res;
        }

        public List<ShowSliders> GetAllDefaultSliders()
        {
            List<ShowSliders> res = new List<ShowSliders>();
            foreach (var r in _repositoryContext.Sliders.Where(r => r.IsDelete == false &&
                    r.IsActive == true &&
                    r.IsDefault == true)
                .ToList())
            {
                if (_repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive == true)
                {
                    res.Add(new ShowSliders
                    {
                        Id = r.Id,
                        IsDefault = r.IsDefault,
                        LanguageId = r.LanguageId,
                        LanguageCode = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Code,
                        LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive != false ? _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title : _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title + "(غیرفعال)",
                        URL = r.URL,
                        SlideImage = r.SlideImage,
                        PageId = r.PageId,
                        PageAlias = _repositoryContext.Pages.Where(w => w.Id == r.PageId).Select(w => w.Alias).FirstOrDefault(),
                        PageTitle = _repositoryContext.Pages.Where(w => w.Id == r.PageId).Select(w => w.Name).FirstOrDefault(),
                        CreationDateTime = r.CreationDateTime
                    });
                }
            }
            return res;
        }



        public List<ShowSliders> GetAllSliders(int languageId)
        {
            List<ShowSliders> res = new List<ShowSliders>();
            foreach (var r in _repositoryContext.Sliders.Where(r => r.IsDelete == false &&
                    r.IsActive == true &&
                    (languageId != 0 ? r.LanguageId == languageId : true))
                .ToList())
            {
                if (_repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).IsActive == true)
                {
                    res.Add(new ShowSliders
                    {
                        Id = r.Id,
                        IsDefault = r.IsDefault,
                        LanguageId = r.LanguageId,
                        LanguageCode = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Code,
                        LanguageTitle = _repositoryContext.Languages.FirstOrDefault(w => w.Id == r.LanguageId).Title,
                        URL = r.URL,
                        SlideImage = r.SlideImage,
                        PageId = r.PageId,
                        PageAlias = _repositoryContext.Pages.Where(w => w.Id == r.PageId).Select(w => w.Alias).FirstOrDefault(),
                        PageTitle = _repositoryContext.Pages.Where(w => w.Id == r.PageId).Select(w => w.Name).FirstOrDefault(),
                        CreationDateTime = r.CreationDateTime
                    });
                }
            }
            return res;
        }



        public Slider GetSliderById(int Id)
        {
            return FindByCondition(w => w.Id == Id).FirstOrDefault();
        }

        public string ShowSlideImage(int Id)
        {
            return (from r in _repositoryContext.Sliders
                    where r.Id == Id && r.IsDelete == false
                    select r.SlideImage).FirstOrDefault();
        }

        public void UpdateSlider(Slider slider)
        {
            Update(slider);
            Save();
        }
    }
}
