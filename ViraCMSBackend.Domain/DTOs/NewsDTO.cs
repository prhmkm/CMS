

namespace ViraCMSBackend.Domain.DTOs
{
    public class NewsDTO
    {
        public class AddNewsRequest
        {
            public int LanguageId { get; set; }
            public string Title { get; set; }
            public string ShortText { get; set; }
            public string ThumbPhoto { get; set; }
            public string ThumbPhotoName { get; set; }
            public string CustomHTML { get; set; }
        }
        public class EditNewsRequest
        {
            public int Id { get; set; }
            public int? LanguageId { get; set; }
            public string? Title { get; set; }
            public string? ShortText { get; set; }
            public string? ThumbPhoto { get; set; }
            public string? ThumbPhotoName { get; set; }
            public string? CustomHTML { get; set; }
            public bool? IsActive { get; set; }
        }
        public class ShowNews
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string LanguageTitle { get; set; }
            public string Title { get; set; }
            public string Alias { get; set; }
            public string DateTime { get; set; }
            public string ShortText { get; set; }
            public string ThumbPhoto { get; set; }
            public string CustomHTML { get; set; }
            public int ViewCounter { get; set; }
        }
        public class BOShowNews
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string LanguageTitle { get; set; }
            public string Title { get; set; }
            public string Alias { get; set; }
            public string DateTime { get; set; }
            public string ShortText { get; set; }
            public string ThumbPhoto { get; set; }
            public string CustomHTML { get; set; }
            public int ViewCounter { get; set; }
            public bool IsActive { get; set; }
        }
        public class DeleteNews
        {
            public int Id { get; set; }
        }
        public class GetNewsByAlias
        {
            public string Alias { get; set; }
        }
    }
}
