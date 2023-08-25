namespace LibraryManagementMvcUI.Areas.AdminPanel.Models.ApiTypes
{
    public class BorrowedBookItem
    {
        public int BorrowedBookId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool Returned { get; set; }
    }
}
