namespace LibraryManagementMvcUI.Areas.AdminPanel.Models.Dtos.User
{
    public class UpdateUserDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
