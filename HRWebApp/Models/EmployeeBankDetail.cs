using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRWebApp.Models
{
    public class EmployeeBankDetail
    {
        [Key]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Required]
        [MaxLength(100)]
        public string BankName { get; set; }

        [Required]
        [MaxLength(20)]
        public string AccountNumber { get; set; }

        [Required]
        [MaxLength(11)]
        public string IFSCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string BranchName { get; set; }

        [MaxLength(20)]
        public string EPFONumber { get; set; }

        [MaxLength(20)]
        public string UANNumber { get; set; }

        [MaxLength(20)]
        public string ESICNumber { get; set; }
    }
}
