using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace HRWebApp.Models
{
    public class PageAccess
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Role")]
        public string RoleId { get; set; }
        public IdentityRole Role { get; set; }

        [Required]
        [MaxLength(100)]
        public string PageName { get; set; } // Controller/Action or Route

        public bool CanView { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? LastModifiedDate { get; set; }
    }
}
