namespace HRWebApp.DTOs
{
    public class SalarySlipDTO
    {
        public string MonthYear { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal TotalAllowances { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal NetSalary { get; set; }
        public string DownloadLink { get; set; }
    }
}