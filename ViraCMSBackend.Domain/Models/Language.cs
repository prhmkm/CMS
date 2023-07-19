﻿using System;
using System.Collections.Generic;

namespace ViraCMSBackend.Domain.Models
{
    public partial class Language
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Flag { get; set; } = null!;
        public bool? IsRTL { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
