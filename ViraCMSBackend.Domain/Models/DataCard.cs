using System;
using System.Collections.Generic;

namespace ViraCMSBackend.Domain.Models
{
    public partial class DataCard
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string Title { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreationDateTime { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
