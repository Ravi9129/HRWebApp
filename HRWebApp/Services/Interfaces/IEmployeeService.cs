using HRWebApp.DTOs;

namespace HRWebApp.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDetailDTO>> GetAllEmployeesAsync();
        Task<EmployeeDetailDTO> GetEmployeeByIdAsync(int id);
        Task<EmployeeDetailDTO> GetEmployeeByUserIdAsync(string userId);
        Task CreateEmployeeAsync(CreateEmployeeDTO employeeDTO);
        Task UpdateEmployeeAsync(int id, UpdateEmployeeDTO employeeDTO);
        Task DeleteEmployeeAsync(int id);
        Task<int> GetActiveEmployeeCountAsync();
        Task<int> GetInactiveEmployeeCountAsync();
        Task<IEnumerable<EmployeeDetailDTO>> GetEmployeesByDepartmentAsync(int departmentId);
        Task<EmployeeShiftDTO> GetEmployeeShiftAsync(int employeeId);
        Task UpdateEmployeeShiftAsync(int employeeId, EmployeeShiftDTO shiftDTO);
        Task<EmployeeBankDetailDTO> GetEmployeeBankDetailAsync(int employeeId);
        Task UpdateEmployeeBankDetailAsync(int employeeId, EmployeeBankDetailDTO bankDetailDTO);
        Task<IEnumerable<EmployeeBenefitDTO>> GetEmployeeBenefitsAsync(int employeeId);
        Task AddEmployeeBenefitAsync(int employeeId, EmployeeBenefitDTO benefitDTO);
        Task RemoveEmployeeBenefitAsync(int benefitId);
    }
}
