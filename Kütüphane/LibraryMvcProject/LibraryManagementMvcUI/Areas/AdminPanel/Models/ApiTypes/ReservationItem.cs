namespace LibraryManagementMvcUI.Areas.AdminPanel.Models.ApiTypes
{
    public class ReservationItem
    {

        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public bool Active { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
