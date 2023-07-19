using System;
using System.Collections.Generic;

namespace ViraCMSBackend.Domain.Models
{
    public partial class FixedBasicSetting
    {
        public int Id { get; set; }
        public string FavoriteIcon { get; set; } = null!;
        public string Logo { get; set; } = null!;
        public string FooterLogo { get; set; } = null!;
        public string FirstColor { get; set; } = null!;
        public string SecondColor { get; set; } = null!;
        public string ThirdColor { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? InstagramAddress { get; set; }
        public string? TelegramAddress { get; set; }
        public string? WhatsAppAddress { get; set; }
        public string? LinkedinAddress { get; set; }
        public DateTime CreationDateTime { get; set; }
        public bool IsDelete { get; set; }
    }
}
