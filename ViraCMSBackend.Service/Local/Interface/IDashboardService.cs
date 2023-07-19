using static ViraCMSBackend.Domain.DTOs.DashboardDTO;

namespace ViraCMSBackend.Service.Local.Interface
{
    public interface IDashboardService
    {
        List<MostViewedBlogs> GetMostViwedBlogs(int langId);
        List<MostViewedNews> GetMostViwedNews(int langId);
        List<MostViewedPages> GetMostViwedPages(int langId);
        List<MostCountryVisits> GetMostCountryVisits();
    }
}
