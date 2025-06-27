using System.ComponentModel.DataAnnotations;

namespace QuanPhucLongQuang_DoAnWeb.Models
{
    public class Borrow
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public DateTime BorrowDate { get; set; }

        public bool IsReturned { get; set; }

        public ICollection<BorrowDetail> BorrowDetails { get; set; }
    }
}
