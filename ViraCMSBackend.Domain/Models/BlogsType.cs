using System;
using System.Collections.Generic;

namespace ViraCMSBackend.Domain.Models
{
    public partial class BlogsType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreationDateTime { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
