using System.Collections.Generic;

namespace QuanPhucLongQuang_DoAnWeb.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
} 