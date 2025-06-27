using System.ComponentModel.DataAnnotations.Schema;

namespace QuanPhucLongQuang_DoAnWeb.Models
{
    public class ReturnDetail
    {
        public int Id { get; set; }

        public int ReturnId { get; set; }
        [ForeignKey("ReturnId")]
        public Return Return { get; set; }

        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }

        public int Quantity { get; set; }
    }
}
