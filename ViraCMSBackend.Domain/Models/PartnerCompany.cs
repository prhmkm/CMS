using System;
using System.Collections.Generic;

namespace ViraCMSBackend.Domain.Models
{
    public partial class PartnerCompany
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; } = null!;
        public string ThumbPhoto { get; set; } = null!;
        public string HLink { get; set; } = null!;
        public DateTime CreationDateTime { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
