namespace LibraryManagementMvcUI.Areas.AdminPanel.Models.ApiTypes
{
    public class AuthorItem
    {
        public string AuthorName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? PicturePath { get; set; }
        public int AuthorId { get; set; }

    }
}
