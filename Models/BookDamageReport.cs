using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanPhucLongQuang_DoAnWeb.Models
{
    public class BookDamageReport
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int? ReturnId { get; set; }
        public Return Return { get; set; }

        [Required]
        public string Description { get; set; } // Ví dụ: "Rách trang 10-15"

        public DateTime ReportDate { get; set; } = DateTime.Now;

        public bool IsResolved { get; set; } = false;

        public decimal? FineAmount { get; set; } // Nếu có
    }
} 