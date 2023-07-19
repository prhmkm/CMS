

using System.Reflection.Metadata.Ecma335;

namespace ViraCMSBackend.Domain.DTOs
{
    public class PartnerCompanyDTO
    {
        public class ShowPartnerCompanies
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string LanguageTitle { get; set; }
            public string Name { get; set; }
            public string ThumbPhoto { get; set; }
            public string HLink { get; set; }
        }
        public class BOShowPartnerCompanies
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string LanguageTitle { get; set; }
            public string Name { get; set; }
            public string ThumbPhoto { get; set; }
            public string HLink { get; set; }
            public bool IsActive { get; set; }
        }
        public class AddPartnerCompany
        {
            public int LanguageId { get; set; }
            public string Name { get; set; }
            public string ThumbPhoto { get; set; }
            public string ThumbPhotoName { get; set; }
            public string HLink { get; set; }
        }
        public class EditPartnerCompany
        {
            public int Id { get; set; }
            public int? LanguageId { get; set; }
            public string? Name { get; set; }
            public string? ThumbPhoto { get; set; }
            public string? ThumbPhotoName { get; set; }
            public string? HLink { get; set; }
            public bool? IsActive { get; set; }
        }
        public class DeletePartnerCompany
        {
            public int Id { get; set; }
        }
    }
}
