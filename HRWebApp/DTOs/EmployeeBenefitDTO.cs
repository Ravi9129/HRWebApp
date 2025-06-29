namespace HRWebApp.DTOs
{
    public class EmployeeBenefitDTO
    {
        public int Id { get; set; }
        public string BenefitType { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public bool IsTaxable { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
