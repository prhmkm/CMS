using System;
using System.Collections.Generic;

namespace ViraCMSBackend.Domain.Models
{
    public partial class Menu
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string Title { get; set; } = null!;
        public int ParentId { get; set; }
        public string? HLink { get; set; }
        public int? PageId { get; set; }
        public int Ordering { get; set; }
        public DateTime CreationDateTime { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
