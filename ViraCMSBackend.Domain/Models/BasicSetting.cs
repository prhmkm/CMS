using System;
using System.Collections.Generic;

namespace ViraCMSBackend.Domain.Models
{
    public partial class BasicSetting
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string Title { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string MobileNumber { get; set; } = null!;
        public string WorkingHours { get; set; } = null!;
        public string Fax { get; set; } = null!;
        public string FooterText { get; set; } = null!;
        public string AboutUs { get; set; } = null!;
        public DateTime CreationDateTime { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
