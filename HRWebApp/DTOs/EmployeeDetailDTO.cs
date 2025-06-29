namespace HRWebApp.DTOs
{
    public class EmployeeDetailDTO
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public int DepartmentId { get; set; }
        public string Role { get; set; }
        public string RoleId { get; set; }
        public DateTime JoiningDate { get; set; }
        public bool IsActive { get; set; }
        public EmployeeShiftDTO Shift { get; set; }
        public EmployeeBankDetailDTO BankDetail { get; set; }
        public IEnumerable<EmployeeBenefitDTO> Benefits { get; set; }

        public string FullName => $"{FirstName} {MiddleName} {LastName}".Replace("  ", " ");
    }
}
