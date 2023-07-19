﻿using System;
using System.Collections.Generic;

namespace ViraCMSBackend.Domain.Models
{
    public partial class Picture
    {
        public long Id { get; set; }
        public string? ImageName { get; set; }
        public string? Address { get; set; }
        public string? Thumbnail { get; set; }
    }
}
