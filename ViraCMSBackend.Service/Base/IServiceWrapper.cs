using ViraCMSBackend.Service.Local.Interface;
using ViraCMSBackend.Service.Remote.Interfaces;

namespace ViraCMSBackend.Service.Base
{
    public interface IServiceWrapper
    {       
        IUserService User { get; }
        ISliderService Slider { get; }
        IIpconverterService IpconverterService { get; }
        ILanguageService Language { get; }
        IBasicSettingService BasicSetting { get; }
        IColorService Color { get; }
        IFixedBasicSettingService FixedBasicSetting { get; }
        IBlogService Blog { get; }
        INewsService News { get; }
        ICounterService Counter { get; }
        IContentCardService ContentCard { get; }
        IDataCardService DataCard { get; }
        IMenuService Menu { get; }
        IPictureService Picture { get; }
        IPageService Page { get; }
        IContactUsService ContactUs { get; }
        IResultService Result { get; }
        IVisitService Visit { get; }
        IDashboardService Dashboard { get; }
        IPartnerCompanyService PartnerCompany { get; }


        void Save();
    }
}
