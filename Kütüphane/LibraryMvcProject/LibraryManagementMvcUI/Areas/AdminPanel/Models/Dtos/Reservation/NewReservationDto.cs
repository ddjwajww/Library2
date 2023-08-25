namespace LibraryManagementMvcUI.Areas.AdminPanel.Models.Dtos.Reservation
{
    public class NewReservationDto
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool Active { get; set; }
    }
}
