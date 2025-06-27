using System.ComponentModel.DataAnnotations.Schema;

namespace QuanPhucLongQuang_DoAnWeb.Models
{
    public class Return
    {
        public int Id { get; set; }

        public int BorrowId { get; set; }

        [ForeignKey("BorrowId")]
        public Borrow Borrow { get; set; }

        public DateTime ReturnDate { get; set; }

        public ICollection<ReturnDetail> ReturnDetails { get; set; }
    }
}
