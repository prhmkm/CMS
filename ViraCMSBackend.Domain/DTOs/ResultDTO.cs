using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ViraCMSBackend.Domain.DTOs.BlogDTO;
using static ViraCMSBackend.Domain.DTOs.NewsDTO;

namespace ViraCMSBackend.Domain.DTOs
{
    public class ResultDTO
    {
        public class ShowResult
        {
            public int LanguageId { get; set; }
            public string LanguageTitle { get; set; }
            public string Title { get; set; }
            public string Alias { get; set; }
            public string Type { get; set; }
        }
        public class GetResultRequest
        {
            public string text { get; set; }
        }
    }
}
