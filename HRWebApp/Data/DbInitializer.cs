using HRWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRWebApp.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            // Apply pending migrations
            await context.Database.MigrateAsync();

            // Seed roles if they don't exist
            await SeedRoles(roleManager);

            // Seed admin user if doesn't exist
            await SeedAdminUser(userManager);

            // Seed departments if none exist
            await SeedDepartments(context);

            // Seed product categories if none exist
            await SeedProductCategories(context);

            // Seed page access permissions
            await SeedPageAccessPermissions(context, roleManager);
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "HR", "Manager", "Employee" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        private static async Task SeedAdminUser(UserManager<ApplicationUser> userManager)
        {
            var adminEmail = "admin@hrwebapp.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "ADM00001",
                    Email = adminEmail,
                    FirstName = "Admin",
                    LastName = "User",
                    EmailConfirmed = true,
                    IsActive = true,
                    CreatedDate = new DateTime(2023, 1, 1)
                };

                var createResult = await userManager.CreateAsync(user, "Admin@123");
                if (createResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }

        private static async Task SeedDepartments(ApplicationDbContext context)
        {
            if (!await context.Departments.AnyAsync())
            {
                var departments = new List<Department>
                {
                    new Department {
                        Id = 1,
                        Name = "Human Resources",
                        Description = "HR Department",
                        IsActive = true,
                        CreatedDate = new DateTime(2023, 1, 1)
                    },
                    new Department {
                        Id = 2,
                        Name = "Information Technology",
                        Description = "IT Department",
                        IsActive = true,
                        CreatedDate = new DateTime(2023, 1, 1)
                    },
                    new Department {
                        Id = 3,
                        Name = "Finance",
                        Description = "Finance Department",
                        IsActive = true,
                        CreatedDate = new DateTime(2023, 1, 1)
                    },
                    new Department {
                        Id = 4,
                        Name = "Operations",
                        Description = "Operations Department",
                        IsActive = true,
                        CreatedDate = new DateTime(2023, 1, 1)
                    },
                    new Department {
                        Id = 5,
                        Name = "Marketing",
                        Description = "Marketing Department",
                        IsActive = true,
                        CreatedDate = new DateTime(2023, 1, 1)
                    }
                };

                await context.Departments.AddRangeAsync(departments);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedProductCategories(ApplicationDbContext context)
        {
            if (!await context.ProductCategories.AnyAsync())
            {
                var categories = new List<ProductCategory>
                {
                    new ProductCategory {
                        Id = 1,
                        Name = "Electronics",
                        Description = "Electronic items",
                        IsActive = true
                    },
                    new ProductCategory {
                        Id = 2,
                        Name = "Stationery",
                        Description = "Office stationery items",
                        IsActive = true
                    },
                    new ProductCategory {
                        Id = 3,
                        Name = "Furniture",
                        Description = "Office furniture",
                        IsActive = true
                    },
                    new ProductCategory {
                        Id = 4,
                        Name = "Software",
                        Description = "Software licenses",
                        IsActive = true
                    }
                };

                await context.ProductCategories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedPageAccessPermissions(ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            if (!await context.PageAccesses.AnyAsync())
            {
                var adminRole = await roleManager.FindByNameAsync("Admin");
                var hrRole = await roleManager.FindByNameAsync("HR");
                var managerRole = await roleManager.FindByNameAsync("Manager");
                var employeeRole = await roleManager.FindByNameAsync("Employee");

                var pageAccesses = new List<PageAccess>
                {
                    // Admin permissions
                    new PageAccess {
                        Id = 1,
                        RoleId = adminRole.Id,
                        PageName = "Admin/Dashboard",
                        CanView = true,
                        CanCreate = true,
                        CanEdit = true,
                        CanDelete = true,
                        CreatedDate = new DateTime(2023, 1, 1)
                    },
                    new PageAccess {
                        Id = 2,
                        RoleId = adminRole.Id,
                        PageName = "Admin/Employees",
                        CanView = true,
                        CanCreate = true,
                        CanEdit = true,
                        CanDelete = true,
                        CreatedDate = new DateTime(2023, 1, 1)
                    },
                    new PageAccess {
                        Id = 3,
                        RoleId = adminRole.Id,
                        PageName = "Admin/Departments",
                        CanView = true,
                        CanCreate = true,
                        CanEdit = true,
                        CanDelete = true,
                        CreatedDate = new DateTime(2023, 1, 1)
                    },
                    new PageAccess {
                        Id = 4,
                        RoleId = adminRole.Id,
                        PageName = "Admin/Products",
                        CanView = true,
                        CanCreate = true,
                        CanEdit = true,
                        CanDelete = true,
                        CreatedDate = new DateTime(2023, 1, 1)
                    },
                    new PageAccess {
                        Id = 5,
                        RoleId = adminRole.Id,
                        PageName = "Admin/Roles",
                        CanView = true,
                        CanCreate = true,
                        CanEdit = true,
                        CanDelete = true,
                        CreatedDate = new DateTime(2023, 1, 1)
                    },
                    new PageAccess {
                        Id = 6,
                        RoleId = adminRole.Id,
                        PageName = "Admin/PageAccess",
                        CanView = true,
                        CanCreate = true,
                        CanEdit = true,
                        CanDelete = true,
                        CreatedDate = new DateTime(2023, 1, 1)
                    },
                    new PageAccess {
                        Id = 7,
                        RoleId = adminRole.Id,
                        PageName = "Admin/Reports",
                        CanView = true,
                        CanCreate = true,
                        CanEdit = true,
                        CanDelete = true,
                        CreatedDate = new DateTime(2023, 1, 1)
                    },

                    // HR permissions
                    new PageAccess {
                        Id = 8,
                        RoleId = hrRole.Id,
                        PageName = "HR/Dashboard",
                        CanView = true,
                        CanCreate = false,
                        CanEdit = false,
                        CanDelete = false,
                        CreatedDate = new DateTime(2023, 1, 1)
                    },
                    new PageAccess {
                        Id = 9,
                        RoleId = hrRole.Id,
                        PageName = "HR/Employees",
                        CanView = true,
                        CanCreate = true,
                        CanEdit = true,
                        CanDelete = false,
                        CreatedDate = new DateTime(2023, 1, 1)
                    },
                    new PageAccess {
                        Id = 10,
                        RoleId = hrRole.Id,
                        PageName = "HR/Departments",
                        CanView = true,
                        CanCreate = true,
                        CanEdit = true,
                        CanDelete = false,
                        CreatedDate = new DateTime(2023, 1, 1)
                    },

                    // Manager permissions
                    new PageAccess {
                        Id = 11,
                        RoleId = managerRole.Id,
                        PageName = "Manager/Dashboard",
                        CanView = true,
                        CanCreate = false,
                        CanEdit = false,
                        CanDelete = false,
                        CreatedDate = new DateTime(2023, 1, 1)
                    },
                    new PageAccess {
                        Id = 12,
                        RoleId = managerRole.Id,
                        PageName = "Manager/Employees",
                        CanView = true,
                        CanCreate = false,
                        CanEdit = false,
                        CanDelete = false,
                        CreatedDate = new DateTime(2023, 1, 1)
                    },
                    new PageAccess {
                        Id = 13,
                        RoleId = managerRole.Id,
                        PageName = "Manager/Reports",
                        CanView = true,
                        CanCreate = false,
                        CanEdit = false,
                        CanDelete = false,
                        CreatedDate = new DateTime(2023, 1, 1)
                    },

                    // Employee permissions
                    new PageAccess {
                        Id = 14,
                        RoleId = employeeRole.Id,
                        PageName = "Employee/Dashboard",
                        CanView = true,
                        CanCreate = false,
                        CanEdit = false,
                        CanDelete = false,
                        CreatedDate = new DateTime(2023, 1, 1)
                    },
                    new PageAccess {
                        Id = 15,
                        RoleId = employeeRole.Id,
                        PageName = "Employee/Profile",
                        CanView = true,
                        CanCreate = false,
                        CanEdit = true,
                        CanDelete = false,
                        CreatedDate = new DateTime(2023, 1, 1)
                    },
                    new PageAccess {
                        Id = 16,
                        RoleId = employeeRole.Id,
                        PageName = "Employee/SalarySlips",
                        CanView = true,
                        CanCreate = false,
                        CanEdit = false,
                        CanDelete = false,
                        CreatedDate = new DateTime(2023, 1, 1)
                    }
                };

                await context.PageAccesses.AddRangeAsync(pageAccesses);
                await context.SaveChangesAsync();
            }
        }
    }
}