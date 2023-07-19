using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViraCMSBackend.Domain.DTOs
{
    public class SliderDTO
    {
        public class AddSliderRequest
        {
            public int LanguageId { get; set; }
            public string SlideImage { get; set; } 
            public string SlideImageName { get; set; } 
            public string? URL { get; set; }
            public int? PageId { get; set; }
            public bool? IsDefault { get; set; }
        }
        public class UpdateSliderRequest
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string? SlideImage { get; set; }
            public string? SlideImageName { get; set; }
            public string? URL { get; set; }
            public int? PageId { get; set; }
            public bool? IsDefault { get; set; }
            public bool? IsActive { get; set; }
        }
        public class DeleteSliderRequest
        {
            public int Id { get; set; }
        }
        public class ShowSliders
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string SlideImage { get; set; }
            public string? URL { get; set; }
            public int? PageId { get; set; }
            public string? PageTitle { get; set; }
            public string? PageAlias { get; set; }
            public string LanguageCode { get; set; }
            public string LanguageTitle { get; set; }
            public bool? IsDefault { get; set; }
            public DateTime? CreationDateTime { get; set; }
        }
        public class BOShowSliders
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string? URL { get; set; }
            public int? PageId { get; set; }
            public string? PageTitle { get; set; }
            public string? PageAlias { get; set; }
            public string LanguageCode { get; set; }
            public string LanguageTitle { get; set; }
            public bool? IsDefault { get; set; }
            public DateTime? CreationDateTime { get; set; }
            public bool? IsActive { get; set; }
        }
    }
}
