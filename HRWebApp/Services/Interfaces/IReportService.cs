using HRWebApp.DTOs;

namespace HRWebApp.Services.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<EmployeeReportDTO>> GenerateEmployeeReportAsync();
        Task<IEnumerable<DepartmentReportDTO>> GenerateDepartmentReportAsync();
        Task<IEnumerable<SalaryReportDTO>> GenerateSalaryReportAsync(int? month, int? year);
        Task<byte[]> ExportEmployeeReportToExcelAsync();
        Task<byte[]> ExportDepartmentReportToExcelAsync();
        Task<byte[]> ExportSalaryReportToExcelAsync(int? month, int? year);
    }
}
