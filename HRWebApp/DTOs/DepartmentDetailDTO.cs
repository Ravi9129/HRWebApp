namespace HRWebApp.DTOs
{
    public class DepartmentDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ManagerName { get; set; }
        public string? ManagerId { get; set; }
        public int EmployeeCount { get; set; }
        public bool IsActive { get; set; }
    }
}
