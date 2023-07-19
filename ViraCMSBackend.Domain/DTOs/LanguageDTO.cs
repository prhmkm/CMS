using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViraCMSBackend.Domain.DTOs
{
    public class LanguageDTO
    {
        public class AddLanguageRequest
        {
            public string Title { get; set; }
            public string Code { get; set; }
            public string Flag { get; set; }
            public bool IsRTL { get; set; }
        }
        public class EditLanguageRequest
        {
            public int Id { get; set; }
            public string? Title { get; set; }
            public string? Code { get; set; }
            public string? Flag { get; set; }
            public bool? IsRTL { get; set; }
            public bool? IsActive { get; set; }
        }
        public class DeleteLanguage
        {
            public int Id { get; set; }
        }
    }
}
