namespace LibraryManagementMvcUI.Areas.AdminPanel.Models.Dtos.Category
{
    public class UpdateCategoryDto
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
    }
}
