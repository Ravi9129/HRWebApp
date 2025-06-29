using System.ComponentModel.DataAnnotations;

namespace HRWebApp.DTOs
{
    public class UpdateEmployeeDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public string RoleId { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
