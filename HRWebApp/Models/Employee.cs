using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace HRWebApp.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 5)]
        public string EmployeeId { get; set; } // 5-digit format: 00001

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [Required]
        [ForeignKey("Role")]
        public string RoleId { get; set; }
        public IdentityRole Role { get; set; }

        public DateTime JoiningDate { get; set; } = DateTime.UtcNow;
        public DateTime? LeavingDate { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public EmployeeShift Shift { get; set; }
        public EmployeeBankDetail BankDetail { get; set; }
        public ICollection<EmployeeBenefit> Benefits { get; set; }
        public ICollection<SalarySlip> SalarySlips { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
