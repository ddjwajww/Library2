using LibraryManagementMvcUI.Areas.AdminPanel.Models.ApiTypes;
using Microsoft.Build.Evaluation;

namespace LibraryManagementMvcUI.Areas.AdminPanel.Models.Dtos.Book
{
    public class NewBookDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }        
        public int TotalCopies { get; set; }        
        public int AvailableCopies { get; set; }        
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }



    }
}
