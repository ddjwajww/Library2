namespace LibraryManagementMvcUI.Areas.AdminPanel.Models.Dtos.BorrowedBook
{
    public class UpdateBorrowedBookDto
    {
        public int BorrowedBookId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool Returned { get; set; }
    }
}
