namespace LibraryManagementMvcUI.Areas.AdminPanel.Models.Dtos.Book
{
    public class UpdateBookDto
    {
        public int BookId { get; set; }
        public string PicturePath { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
        public string? CategoryName { get; set; }
        public string? AuthorName { get; set; }
    }
}
