namespace LibraryManagementMvcUI.Areas.AdminPanel.Models.Dtos.BorrowedBook
{
    public class NewBorrowedBookDto
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool Returned { get; set; }
    }
}
