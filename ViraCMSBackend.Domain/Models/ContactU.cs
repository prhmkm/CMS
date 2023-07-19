using System;
using System.Collections.Generic;

namespace ViraCMSBackend.Domain.Models
{
    public partial class ContactU
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string MobileNumber { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int StateId { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
