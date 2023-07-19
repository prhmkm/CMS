
namespace ViraCMSBackend.Domain.DTOs
{
    public class DashboardDTO
    {
        public class MostViewedBlogs
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string LanguageTitle { get; set; }
            public string Title { get; set; }
            public int ViewCounter { get; set; }
        }
        public class MostViewedNews
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string LanguageTitle { get; set; }
            public string Title { get; set; }
            public int ViewCounter { get; set; }
        }
        public class MostViewedPages
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string LanguageTitle { get; set; }
            public string Name { get; set; }
            public int ViewCounter { get; set; }
        }
        public class MostCountryVisits
        {
            public string Country { get; set; } 
            public int ViewCounter { get; set; }
        }
    }
}
