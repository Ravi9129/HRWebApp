using System.ComponentModel.DataAnnotations;

namespace HRWebApp.DTOs
{
    public class UpdatePageAccessDTO
    {
        [Required]
        public bool CanView { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }
}
