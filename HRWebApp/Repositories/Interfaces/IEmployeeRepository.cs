using HRWebApp.DTOs;
using HRWebApp.Models;

namespace HRWebApp.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeDetailDTO>> GetAllEmployeesAsync();
        Task<EmployeeDetailDTO> GetEmployeeByIdAsync(int id);
        Task<EmployeeDetailDTO> GetEmployeeByUserIdAsync(string userId);
        Task CreateEmployeeAsync(CreateEmployeeDTO employeeDTO);
        Task UpdateEmployeeAsync(int id, UpdateEmployeeDTO employeeDTO);
        Task DeleteEmployeeAsync(int id);
        Task<bool> EmployeeExistsAsync(int id);
        Task<int> GetActiveEmployeeCountAsync();
        Task<int> GetInactiveEmployeeCountAsync();
        Task<IEnumerable<EmployeeDetailDTO>> GetEmployeesByDepartmentAsync(int departmentId);
        Task<EmployeeShift> GetEmployeeShiftAsync(int employeeId);
        Task UpdateEmployeeShiftAsync(int employeeId, EmployeeShiftDTO shiftDTO);
        Task<EmployeeBankDetail> GetEmployeeBankDetailAsync(int employeeId);
        Task UpdateEmployeeBankDetailAsync(int employeeId, EmployeeBankDetailDTO bankDetailDTO);
        Task<IEnumerable<EmployeeBenefit>> GetEmployeeBenefitsAsync(int employeeId);
        Task AddEmployeeBenefitAsync(int employeeId, EmployeeBenefitDTO benefitDTO);
        Task RemoveEmployeeBenefitAsync(int benefitId);
    }
}
