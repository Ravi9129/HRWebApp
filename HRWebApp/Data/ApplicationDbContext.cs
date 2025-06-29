using HRWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HRWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<EmployeeShift> EmployeeShifts { get; set; }
        public DbSet<EmployeeBankDetail> EmployeeBankDetails { get; set; }
        public DbSet<EmployeeBenefit> EmployeeBenefits { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<PageAccess> PageAccesses { get; set; }
        public DbSet<SalarySlip> SalarySlips { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure decimal precision for all decimal properties
            builder.Entity<EmployeeBenefit>()
                .Property(e => e.Amount)
                .HasPrecision(18, 2);

            builder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            builder.Entity<SalarySlip>()
                .Property(s => s.BasicSalary)
                .HasPrecision(18, 2);

            builder.Entity<SalarySlip>()
                .Property(s => s.HouseRentAllowance)
                .HasPrecision(18, 2);

            builder.Entity<SalarySlip>()
                .Property(s => s.TravelAllowance)
                .HasPrecision(18, 2);

            builder.Entity<SalarySlip>()
                .Property(s => s.MedicalAllowance)
                .HasPrecision(18, 2);

            builder.Entity<SalarySlip>()
                .Property(s => s.ProvidentFund)
                .HasPrecision(18, 2);

            builder.Entity<SalarySlip>()
                .Property(s => s.TaxDeducted)
                .HasPrecision(18, 2);

            builder.Entity<SalarySlip>()
                .Property(s => s.OtherDeductions)
                .HasPrecision(18, 2);

            builder.Entity<SalarySlip>()
                .Property(s => s.OtherAllowances)
                .HasPrecision(18, 2);

            builder.Entity<SalarySlip>()
                .Property(s => s.NetSalary)
                .HasPrecision(18, 2);

            // Configure relationships and constraints
            builder.Entity<Employee>()
                .HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<Employee>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Employee>()
                .HasOne(e => e.Shift)
                .WithOne(s => s.Employee)
                .HasForeignKey<EmployeeShift>(s => s.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Employee>()
                .HasOne(e => e.BankDetail)
                .WithOne(b => b.Employee)
                .HasForeignKey<EmployeeBankDetail>(b => b.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Employee>()
                .HasMany(e => e.Benefits)
                .WithOne(b => b.Employee)
                .HasForeignKey(b => b.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Employee>()
                .HasMany(e => e.SalarySlips)
                .WithOne(s => s.Employee)
                .HasForeignKey(s => s.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Department>()
                .HasMany(d => d.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure indexes
            builder.Entity<Employee>()
                .HasIndex(e => e.EmployeeId)
                .IsUnique();

            builder.Entity<Department>()
                .HasIndex(d => d.Name)
                .IsUnique();

            builder.Entity<Product>()
                .HasIndex(p => p.Name);

            builder.Entity<ProductCategory>()
                .HasIndex(pc => pc.Name)
                .IsUnique();

            // Configure default values
            builder.Entity<Employee>()
                .Property(e => e.IsActive)
                .HasDefaultValue(true);

            builder.Entity<Department>()
                .Property(d => d.IsActive)
                .HasDefaultValue(true);

            builder.Entity<Product>()
                .Property(p => p.IsDeleted)
                .HasDefaultValue(false);

            builder.Entity<Product>()
                .Property(p => p.AddedDate)
                .HasDefaultValueSql("GETDATE()");

            builder.Entity<AuditLog>()
                .Property(a => a.Timestamp)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}