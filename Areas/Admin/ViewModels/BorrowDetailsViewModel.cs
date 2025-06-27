using QuanPhucLongQuang_DoAnWeb.Models;
using System.Collections.Generic;

namespace QuanPhucLongQuang_DoAnWeb.Areas.Admin.ViewModels
{
    public class BorrowDetailsViewModel
    {
        public Borrow Borrow { get; set; }
        public ApplicationUser User { get; set; }
        public List<BorrowDetail> BorrowDetails { get; set; }
        public List<int> BookIds { get; set; }
        public List<int> Quantities { get; set; }
    }
} 