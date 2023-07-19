using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViraCMSBackend.Domain.DTOs
{
    public class ContactUsDTO
    {
        public class AddMessageRequest
        {
            public int LanguageId { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string MobileNumber { get; set; }
            public string Subject { get; set; }
            public string Description { get; set; }
        }
        public class ShowMessages
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string LanguageTitle { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string MobileNumber { get; set; }
            public string Subject { get; set; }
            public string Description { get; set; }
            public int StateId { get; set; }
            public string StateTitle { get; set; }
            public string CreationDateTime { get; set; }
        }
    }
}
