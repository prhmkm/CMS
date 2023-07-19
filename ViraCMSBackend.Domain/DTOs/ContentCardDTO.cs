

namespace ViraCMSBackend.Domain.DTOs
{
    public class ContentCardDTO
    {
        public class AddContentCardRequest
        {
            public int LanguageId { get; set; }
            public string Title { get; set; }
            public string Image { get; set; }
            public string ImageName { get; set; }
            public string Description { get; set; }
        }
        public class EditContentCardRequest
        {
            public int Id { get; set; }
            public int? LanguageId { get; set; }
            public string? Title { get; set; }
            public string? Image { get; set; }
            public string? ImageName { get; set; }
            public string? Description { get; set; }
            public bool? IsActive { get; set; }
        }
        public class ShowContentCards
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string LanguageTitle { get; set; }
            public string Title { get; set; }
            public string Image { get; set; }
            public string Description { get; set; }
        }
        public class BOShowContentCards
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string LanguageTitle { get; set; }
            public string Title { get; set; }
            public string Image { get; set; }
            public string Description { get; set; }
            public bool IsActive { get; set; }
        }
        public class DeleteContentCard
        {
            public int Id { get; set; }
        }
    }
}
