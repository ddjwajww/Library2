using LibraryManagementMvcUI.Areas.AdminPanel.Models.ApiTypes;

namespace LibraryManagementMvcUI.Areas.AdminPanel.Models.ViewModels
{
    public class HomeIndexViewModel
    {
        public List<CategoryItem> CategoryList { get; set; }
        public List<BookItem> BooktList { get; set; }
        public List<UserItem> UserList { get; set; }
        public List<AuthorItem> AuthorList { get; set; }

    }
}
