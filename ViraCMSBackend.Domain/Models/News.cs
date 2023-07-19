using System;
using System.Collections.Generic;

namespace ViraCMSBackend.Domain.Models
{
    public partial class News
    {
        public int Id { get; set; }
        public string Alias { get; set; } = null!;
        public int LanguageId { get; set; }
        public string Title { get; set; } = null!;
        public string ShortText { get; set; } = null!;
        public string ThumbPhoto { get; set; } = null!;
        public string CustomHTML { get; set; } = null!;
        public int ViewCounter { get; set; }
        public DateTime CreationDateTime { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
