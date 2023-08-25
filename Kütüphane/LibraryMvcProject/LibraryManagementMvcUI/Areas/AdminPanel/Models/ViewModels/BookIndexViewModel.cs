using LibraryManagementMvcUI.Areas.AdminPanel.Models.ApiTypes;
using Microsoft.Build.Evaluation;

namespace LibraryManagementMvcUI.Areas.AdminPanel.Models.ViewModels
{
    public class BookIndexViewModel
    {

        public List<CategoryItem> CategoryList { get; set; }
        public List<BookItem> BooktList { get; set; }        
        public List<BorrowedBookItem> BorrowedBookList { get; set; }
        public List<ReservationItem> ReservationList { get; set; }
        public int? ActiveCategoryId { get; set; }
        public int? ActiveUserId { get; set; }
    }
}
