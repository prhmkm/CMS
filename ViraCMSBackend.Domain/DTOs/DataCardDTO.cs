

namespace ViraCMSBackend.Domain.DTOs
{
    public class DataCardDTO
    {
        public class AddDataCardRequest
        {
            public int LanguageId { get; set; }
            public string Title { get; set; }
            public string Image { get; set; }
            public string ImageName { get; set; }
            public string Description { get; set; }
        }
        public class EditDataCardRequest
        {
            public int Id { get; set; }
            public int? LanguageId { get; set; }
            public string? Title { get; set; }
            public string? Image { get; set; }
            public string? ImageName { get; set; }
            public string? Description { get; set; }
            public bool? IsActive { get; set; }
        }
        public class ShowDataCards
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string LanguageTitle { get; set; }
            public string Title { get; set; }
            public string Image { get; set; }
            public string Description { get; set; }
        }
        public class BOShowDataCards
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string LanguageTitle { get; set; }
            public string Title { get; set; }
            public string Image { get; set; }
            public string Description { get; set; }
            public bool IsActive { get; set; }
        }
        public class DeleteDataCard
        {
            public int Id { get; set; }
        }
    }
}
