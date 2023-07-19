using ViraCMSBackend.Data.Interface;

namespace ViraCMSBackend.Data.Base
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        ISliderRepository Slider { get; }
        ILanguageRepository Language { get; }
        IIpconverterRepository IpConverter { get; }
        IBasicSettingRepository BasicSetting { get; }
        IColorRepository Color { get; }
        IFixedBasicSettingRepository FixedBasicSetting { get; }
        IBlogRepository Blog { get; }
        INewsRepository News { get; }
        ICounterRepository Counter { get; }
        IContentCardRepository ContentCard { get; }
        IDataCardRepository DataCard { get; }
        IMenuRepository Menu { get; }
        IPictureRepository Picture { get; }
        IPageRepository Page { get; }
        IContactUsRepository ContactUs { get; }
        IResultRepository Result { get; }
        IVisitRepository Visit { get; }
        IDashboardRepository Dashboard { get; }
        IPartnerCompanyRepository PartnerCompany { get; }
        void Save();
    }
}
