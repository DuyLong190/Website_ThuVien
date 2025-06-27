using System.ComponentModel.DataAnnotations;

namespace QuanPhucLongQuang_DoAnWeb.Models
{
    public class BookDamageReportViewModel
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string UserId { get; set; }
        public int ReturnId { get; set; }
        public string Description { get; set; }
        public decimal? FineAmount { get; set; }
        public string Condition { get; set; }
    }
} 