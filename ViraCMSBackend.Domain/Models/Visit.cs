using System;
using System.Collections.Generic;

namespace ViraCMSBackend.Domain.Models
{
    public partial class Visit
    {
        public int Id { get; set; }
        public string Country { get; set; } = null!;
        public string IP { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string LanguageCode { get; set; } = null!;
        public DateTime CreationDateTime { get; set; }
    }
}
