using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace QuanPhucLongQuang_DoAnWeb.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

        public string? ImageUrl { get; set; }

        public string? Condition { get; set; } // Ví dụ: "Bình thường", "Rách bìa", "Thiếu trang"
    }
}
