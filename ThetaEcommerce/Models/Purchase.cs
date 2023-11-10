using System;
using System.Collections.Generic;

namespace ThetaEcommerce.Models
{
    public partial class Purchase
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string? Quantity { get; set; }
        public string? ProductId { get; set; }
        public string? SellerId { get; set; }
        public string? VendorId { get; set; }

        // Navigation properties
        public Seller Seller { get; set; }
        public Vendor Vendor { get; set; }
        public Product Product { get; set; }

    }
}
