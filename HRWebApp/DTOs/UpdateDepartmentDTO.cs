using System.ComponentModel.DataAnnotations;

namespace HRWebApp.DTOs
{
    public class UpdateDepartmentDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public string? ManagerId { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
