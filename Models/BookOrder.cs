using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace QuanPhucLongQuang_DoAnWeb.Models
{
    public class BookOrder
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime OrderDate { get; set; }
        public bool IsPickedUp { get; set; } = false;

        public ICollection<BookOrderDetail> BookOrderDetails { get; set; }
    }
} 