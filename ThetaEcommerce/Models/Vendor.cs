using System;
using System.Collections.Generic;

namespace ThetaEcommerce.Models
{
    public partial class Vendor
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Cnic { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
    }
}
