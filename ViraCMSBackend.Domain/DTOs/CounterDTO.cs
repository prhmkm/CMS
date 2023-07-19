

namespace ViraCMSBackend.Domain.DTOs
{
    public class CounterDTO
    {
        public class AddCounterRequest
        {
            public int LanguageId { get; set; }
            public string Title { get; set; }
            public string Number { get; set; }
            public string Icon { get; set; }
            public string IconName { get; set; }
        }
        public class EditCounterRequest
        {
            public int Id { get; set; }
            public int? LanguageId { get; set; }
            public string? Title { get; set; }
            public string? Number { get; set; }
            public string? Icon { get; set; }
            public string? IconName { get; set; }
            public bool? IsActive { get; set; }
        }
        public class ShowCounters
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string LanguageTitle { get; set; }
            public string Title { get; set; }
            public string Number { get; set; }
            public string Icon { get; set; }
        }
        public class BOShowCounters
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string LanguageTitle { get; set; }
            public string Title { get; set; }
            public string Number { get; set; }
            public string Icon { get; set; }
            public bool IsActive { get; set; }
        }
        public class DeleteCounter
        {
            public int Id { get; set; }
        }
    }
}
