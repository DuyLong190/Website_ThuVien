using System.ComponentModel.DataAnnotations.Schema;

namespace QuanPhucLongQuang_DoAnWeb.Models
{
    public class BookOrderDetail
    {
        public int Id { get; set; }

        public int BookOrderId { get; set; }
        [ForeignKey("BookOrderId")]
        public BookOrder BookOrder { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public int Quantity { get; set; }
    }
} 