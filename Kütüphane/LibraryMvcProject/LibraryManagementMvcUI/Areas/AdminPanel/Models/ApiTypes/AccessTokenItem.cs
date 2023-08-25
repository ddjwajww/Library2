namespace LibraryManagementMvcUI.Areas.AdminPanel.Models.ApiTypes
{
    public class AccessTokenItem
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public List<string> Claims { get; set; }
    }
}
