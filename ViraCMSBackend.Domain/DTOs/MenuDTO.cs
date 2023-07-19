

using ViraCMSBackend.Domain.Models;
using static ViraCMSBackend.Domain.DTOs.PageDTO;

namespace ViraCMSBackend.Domain.DTOs
{
    public class MenuDTO
    {
        public class AddMenuRequest
        {
            public int LanguageId { get; set; }
            public string Title { get; set; }
            public int ParentId { get; set; }
            public string? HLink { get; set; }
            public int? PageId { get; set; }
            public int Ordering { get; set; }
        }
        public class EditMenuRequest
        {
            public int Id { get; set; }
            public int? LanguageId { get; set; }
            public string? Title { get; set; }
            public int? ParentId { get; set; }
            public string? HLink { get; set; }
            public int? PageId { get; set; }
            public int? Ordering { get; set; }
            public bool? IsActive { get; set; }
        }
        public class ShowMenu
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string LanguageTitle { get; set; }
            public string Title { get; set; }
            public int ParentId { get; set; }
            public string? HLink { get; set; }
            public int? PageId { get; set; }
            public string? PageAlias { get; set; }
            public string? PageTitle { get; set; }
            //public ShowPages? Page { get; set; }
            public int Ordering { get; set; }
            public List<ShowMenu> Childs { get; set; }
        }
        public class BOShowMenu
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string LanguageTitle { get; set; }
            public string Title { get; set; }
            public int ParentId { get; set; }
            public string? HLink { get; set; }
            public int? PageId { get; set; }
            public string? PageAlias { get; set; }
            public string? PageTitle { get; set; }
            //public ShowPages? Page { get; set; }
            public int Ordering { get; set; }
            public List<BOShowMenu> Childs { get; set; }
            public bool IsActive { get; set; }
        }
        public class DeleteMenu
        {
            public int Id { get; set; }
        }
    }
}
