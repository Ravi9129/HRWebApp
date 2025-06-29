using System.ComponentModel.DataAnnotations;

namespace HRWebApp.DTOs
{
    public class CreatePageAccessDTO
    {
        [Required]
        public string RoleId { get; set; }

        [Required]
        [StringLength(100)]
        public string PageName { get; set; }

        public bool CanView { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }
}
