public class SalaryReportDTO
{
    public string EmployeeId { get; set; }
    public string FullName { get; set; }
    public string Department { get; set; }
    public decimal BasicSalary { get; set; }
    public decimal TotalAllowances { get; set; }
    public decimal TotalDeductions { get; set; }
    public decimal NetSalary { get; set; }
    public string MonthYear { get; set; }
    public string DownloadLink { get; set; } // Added this property
}