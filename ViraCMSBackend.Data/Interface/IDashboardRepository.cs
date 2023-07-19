using static ViraCMSBackend.Domain.DTOs.DashboardDTO;

namespace ViraCMSBackend.Data.Interface
{
    public interface IDashboardRepository
    {
        List<MostViewedBlogs> GetMostViwedBlogs(int langId); 
        List<MostViewedNews> GetMostViwedNews(int langId); 
        List<MostViewedPages> GetMostViwedPages(int langId);
        List<MostCountryVisits> GetMostCountryVisits();
    }
}
