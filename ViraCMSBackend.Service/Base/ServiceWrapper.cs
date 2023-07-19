using Microsoft.Extensions.Options;
using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Data.Base;
using ViraCMSBackend.Service.Local.Interface;
using ViraCMSBackend.Service.Local.Service;
using ViraCMSBackend.Service.Remote.Interfaces;
using ViraCMSBackend.Service.Remote.Service;

namespace ViraCMSBackend.Service.Base
{
    public class ServiceWrapper : IServiceWrapper
    {
        private readonly IOptions<AppSettings> _appSettings;
        private IRepositoryWrapper _repository;
        public ServiceWrapper(IRepositoryWrapper repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
            _repository = repository;
        }

        public IUserService User => new UserService(_repository, _appSettings);

        public ISliderService Slider => new SliderService(_repository, _appSettings);

        public IIpconverterService IpconverterService => new IpconverterService(_appSettings);

        public ILanguageService Language => new LanguageService(_repository, _appSettings);

        public IBasicSettingService BasicSetting => new BasicSettingService(_repository, _appSettings);

        public IColorService Color => new ColorService(_repository, _appSettings);

        public IFixedBasicSettingService FixedBasicSetting => new FixedBasicSettingService(_repository, _appSettings);

        public IBlogService Blog => new BlogService(_repository, _appSettings);

        public INewsService News => new NewsService(_repository, _appSettings);

        public ICounterService Counter => new CounterService(_repository, _appSettings);

        public IContentCardService ContentCard => new ContentCardService(_repository, _appSettings);

        public IDataCardService DataCard => new DataCardService(_repository, _appSettings);

        public IMenuService Menu => new MenuService(_repository, _appSettings);

        public IPictureService Picture => new PictureService(_repository, _appSettings);

        public IPageService Page => new PageService(_repository, _appSettings);

        public IContactUsService ContactUs => new ContactUsService(_repository, _appSettings);

        public IResultService Result => new ResultService(_repository, _appSettings);

        public IVisitService Visit => new VisitService(_repository, _appSettings);

        public IDashboardService Dashboard => new DashboardService(_repository, _appSettings);

        public IPartnerCompanyService PartnerCompany => new PartnerCompanyService(_repository, _appSettings);

        public void Save()
        {
            _repository.Save();
        }
    }
}
