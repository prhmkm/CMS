

namespace ViraCMSBackend.Domain.DTOs
{
    public class BasicSettingDTO
    {
        public class ShowBasicSttings
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string LanguageTitle { get; set; }          
            public string Title { get; set; }
            public string Address { get; set; }
            public string PhoneNumber { get; set; }
            public string MobileNumber { get; set; }
            public string WorkingHours { get; set; }
            public string Fax { get; set; }
            public string FooterText { get; set; }
            public string AboutUs { get; set; }
        }
        public class BOShowBasicSttings
        {
            public int Id { get; set; }
            public int LanguageId { get; set; }
            public string LanguageTitle { get; set; }           
            public string Title { get; set; }
            public string Address { get; set; }
            public string PhoneNumber { get; set; }
            public string MobileNumber { get; set; }
            public string WorkingHours { get; set; }
            public string Fax { get; set; }
            public string FooterText { get; set; }
            public string AboutUs { get; set; }
            public bool IsActive { get; set; }
        }
        public class AddBasicSttingRequest
        {
            public int LanguageId { get; set; }           
            public string Title { get; set; }
            public string Address { get; set; }
            public string PhoneNumber { get; set; }
            public string MobileNumber { get; set; }
            public string WorkingHours { get; set; }
            public string Fax { get; set; }
            public string FooterText { get; set; }
            public string AboutUs { get; set; }
            public bool IsActive { get; set; }
        }
        public class EditBasicSttingRequest
        {
            public int Id { get; set; }
            public int? LanguageId { get; set; }    
            public string? Title { get; set; }
            public string? Address { get; set; }
            public string? PhoneNumber { get; set; }
            public string? MobileNumber { get; set; }
            public string? WorkingHours { get; set; }
            public string? Fax { get; set; }
            public string? FooterText { get; set; }
            public string? AboutUs { get; set; }
            public bool? IsActive { get; set; }
        }
        public class DeleteBasicSttingRequest
        {
            public int Id { get; set; }
        }
        public class ShowFixedBasicSttings
        {
            public int Id { get; set; }
            public string FavoriteIcon { get; set; }
            public string Logo { get; set; }
            public string FooterLogo { get; set; }
            public string FirstColor { get; set; }
            public string SecondColor { get; set; }
            public string ThirdColor { get; set; }
            public string Email { get; set; }
            public string? InstagramAddress { get; set; }
            public string? TelegramAddress { get; set; }
            public string? WhatsAppAddress { get; set; }
            public string? LinkedinAddress { get; set; }
            //public int MenuLevel { get; set; }
        }
        public class AddFixedBasicSttingRequest
        {
            public string FavoriteIcon { get; set; }
            public string FavoriteIconName { get; set; }
            public string Logo { get; set; }
            public string LogoName { get; set; }
            public string FooterLogo { get; set; }
            public string FooterLogoName { get; set; }
            public string FirstColor { get; set; }
            public string SecondColor { get; set; }
            public string ThirdColor { get; set; }
            public string Email { get; set; }
            public string? InstagramAddress { get; set; }
            public string? TelegramAddress { get; set; }
            public string? WhatsAppAddress { get; set; }
            public string? LinkedinAddress { get; set; }
        }
        public class EditFixedBasicSttingRequest
        {
            public int Id { get; set; }
            public string? FavoriteIcon { get; set; }
            public string? FavoriteIconName { get; set; }
            public string? Logo { get; set; }
            public string? LogoName { get; set; }
            public string? FooterLogo { get; set; }
            public string? FooterLogoName { get; set; }
            public string? FirstColor { get; set; }
            public string? SecondColor { get; set; }
            public string? ThirdColor { get; set; }
            public string? Email { get; set; }
            public string? InstagramAddress { get; set; }
            public string? TelegramAddress { get; set; }
            public string? WhatsAppAddress { get; set; }
            public string? LinkedinAddress { get; set; }
        }
        public class DeleteFixedBasicSttingRequest
        {
            public int Id { get; set; }
        }
    }
}
