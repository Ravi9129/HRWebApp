using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRWebApp.Models
{
    public class SalarySlip
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Required]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal BasicSalary { get; set; }

        [Range(0, double.MaxValue)]
        public decimal HouseRentAllowance { get; set; }

        [Range(0, double.MaxValue)]
        public decimal TravelAllowance { get; set; }

        [Range(0, double.MaxValue)]
        public decimal MedicalAllowance { get; set; }

        [Range(0, double.MaxValue)]
        public decimal ProvidentFund { get; set; }

        [Range(0, double.MaxValue)]
        public decimal TaxDeducted { get; set; }

        [Range(0, double.MaxValue)]
        public decimal OtherDeductions { get; set; }

        [Range(0, double.MaxValue)]
        public decimal OtherAllowances { get; set; }

        [Required]
        public decimal NetSalary { get; set; }

        public DateTime GeneratedDate { get; set; } = DateTime.UtcNow;
        public string? Notes { get; set; }
    }
}
