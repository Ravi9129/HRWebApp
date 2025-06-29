namespace HRWebApp.DTOs
{
    public class EmployeeShiftDTO
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string WeeklyOffDay { get; set; }
        public bool HasLunchBreak { get; set; }
        public TimeSpan? LunchBreakStart { get; set; }
        public TimeSpan? LunchBreakEnd { get; set; }
    }
}
