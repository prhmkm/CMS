using ViraCMSBackend.Core.Model.Base;
using ViraCMSBackend.Data.Interface;
using ViraCMSBackend.Data.Repository;
using ViraCMSBackend.Domain.Models;
using Microsoft.Extensions.Options;

namespace ViraCMSBackend.Data.Base
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ViraCMS_DBContext _repoContext;

        public RepositoryWrapper(ViraCMS_DBContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public IUserRepository User => new UserRepository(_repoContext);

        public ISliderRepository Slider => new SliderRepository(_repoContext);

        public ILanguageRepository Language => new LanguageRepository(_repoContext);

        public IIpconverterRepository IpConverter => new IpconverterRepository(_repoContext);

        public IBasicSettingRepository BasicSetting => new BasicSettingRepository(_repoContext);

        public IColorRepository Color => new ColorRepository(_repoContext);

        public IFixedBasicSettingRepository FixedBasicSetting => new FixedBasicSettingRepository(_repoContext);

        public IBlogRepository Blog => new BlogRepository(_repoContext);

        public INewsRepository News => new NewsRepository(_repoContext);

        public ICounterRepository Counter => new CounterRepository(_repoContext);

        public IContentCardRepository ContentCard => new ContentCardRepository(_repoContext);

        public IDataCardRepository DataCard => new DataCardRepository(_repoContext);

        public IMenuRepository Menu => new MenuRepository(_repoContext);

        public IPictureRepository Picture => new PictureRepository(_repoContext);

        public IPageRepository Page => new PageRepository(_repoContext);

        public IContactUsRepository ContactUs => new ContactUsRepository(_repoContext);

        public IResultRepository Result => new ResultRepository(_repoContext);

        public IVisitRepository Visit => new VisitRepository(_repoContext);

        public IDashboardRepository Dashboard => new DashboardRepository(_repoContext);

        public IPartnerCompanyRepository PartnerCompany => new PartnerCompanyRepository(_repoContext);

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
