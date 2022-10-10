using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Data;
using EmployeeManagement.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.Pages.Admin
{
    //[Authorize(Policy = "RequireAdministratorRole")]
    public class EditModel : PageModel
    {
        private readonly EmployeeManagement.Data.ApplicationDBContext _context;
        private readonly SignInManager<Employee> _signInManager;
        private readonly ILogger<EditModel> _logger;

        public EditModel(EmployeeManagement.Data.ApplicationDBContext context, SignInManager<Employee> signInManager, ILogger<EditModel> logger)
        {
            _context = context;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public Employee Employee { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(id.ToString());
                if (employee == null)
                {
                    return NotFound();
                }
                Employee = employee;
                return Page();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"at {nameof(OnGetAsync)}");
                return BadRequest();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["error"] = "Invalid request";
                    return Page();
                }
                
                var employee = await _context.Employees.FindAsync(Employee.Id);

                // It is only possible to update Employee name and Admin status
                employee.Name = Employee.Name;
                employee.Admin = Employee.Admin;
                await _context.SaveChangesAsync();
                

                TempData["success"] = "Employee details edited successfully";
                return RedirectToPage("./Index");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"at {nameof(OnPostAsync)}");
                //return BadRequest();
                throw;
            }
        }
    }
}
