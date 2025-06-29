using System.ComponentModel.DataAnnotations;

namespace HRWebApp.DTOs
{
    public class CreateEmployeeDTO
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public string RoleId { get; set; }

        public DateTime JoiningDate { get; set; } = DateTime.UtcNow;
    }
}
