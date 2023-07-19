
namespace ViraCMSBackend.Domain.DTOs
{
    public class PageDTO
    {
        public class AddPageRequest
        {
            public int LanguageId { get; set; }
            public string Name { get; set; }
            public string CustomHTML { get; set; }
        }
        public class EditPageRequest
        {
            public int Id { get; set; }
            public int? LanguageId { get; set; }
            public string? Name { get; set; }
            public string? CustomHTML { get; set; }
            public bool? IsActive { get; set; }
        }
        public class ShowPages
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string LanguageTitle { get; set; }
            public string Alias { get; set; }
            public string Name { get; set; }
            public string CustomHTML { get; set; }
            public int ViewCounter { get; set; }
        }
        public class BOShowPages
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string LanguageTitle { get; set; }
            public string Alias { get; set; }
            public string Name { get; set; }
            public string CustomHTML { get; set; }
            public int ViewCounter { get; set; }
            public bool IsActive { get; set; }
        }
        public class DeletePage
        {
            public int Id { get; set; }
        }
        public class GetPageById
        {
            public int Id { get; set; }
        }
        public class GetPageByAlias
        {
            public string Alias { get; set; }
        }
    }
}
