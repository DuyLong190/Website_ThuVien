using System.ComponentModel.DataAnnotations.Schema;

namespace QuanPhucLongQuang_DoAnWeb.Models
{
    public class BorrowDetail
    {
        public int Id { get; set; }

        public int BorrowId { get; set; }
        [ForeignKey("BorrowId")]
        public Borrow Borrow { get; set; }

        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }

        public int Quantity { get; set; }
    }
}
