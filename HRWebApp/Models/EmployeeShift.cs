using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRWebApp.Models
{
    public class EmployeeShift
    {
        [Key]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        [Required]
        [MaxLength(20)]
        public string WeeklyOffDay { get; set; } = "Sunday";

        public bool HasLunchBreak { get; set; } = true;
        public TimeSpan? LunchBreakStart { get; set; }
        public TimeSpan? LunchBreakEnd { get; set; }
    }
}
