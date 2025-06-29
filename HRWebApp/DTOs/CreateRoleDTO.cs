using System.ComponentModel.DataAnnotations;

namespace HRWebApp.DTOs
{
    public class CreateRoleDTO
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
