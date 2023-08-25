namespace LibraryManagementMvcUI.Areas.AdminPanel.Models.Dtos.Author
{
    public class UpdateAuthorDto
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? PicturePath { get; set; }
    }
}
