namespace ViraCMSBackend.Domain.DTOs
{
    public class BlogDTO
    {
        public class AddBlogRequest
        {
            public int LanguageId { get; set; }
            public string Title { get; set; }
            public string ShortText { get; set; }
            public string ThumbPhoto { get; set; }
            public string ThumbPhotoName { get; set; }
            public string CustomHTML { get; set; }
        }
        public class EditBlogRequest
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
        public class ShowBlogs
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
        public class BOShowBlogs
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
        public class DeleteBlog
        {
            public int Id { get; set; }
        }
        public class GetBlogByAlias
        {
            public string Alias { get; set; }
        }
    }
}
