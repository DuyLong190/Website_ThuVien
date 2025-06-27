using System;

namespace QuanPhucLongQuang_DoAnWeb.Models
{
    public class CartItem
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string? ImageUrl { get; set; }
        public int Quantity { get; set; } = 1;
    }
} 