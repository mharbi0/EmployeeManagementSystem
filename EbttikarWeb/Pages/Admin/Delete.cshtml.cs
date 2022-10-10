using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Data;
using EmployeeManagement.Model;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManagement.Pages.Admin
{
    //[Authorize(Policy = "RequireAdministratorRole")]
    public class DeleteModel : PageModel
    {
        private readonly EmployeeManagement.Data.ApplicationDBContext _context;

        public DeleteModel(EmployeeManagement.Data.ApplicationDBContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Employee Employee { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id.ToString());

            if (employee == null)
            {
                return NotFound();
            }
            else 
            {
                Employee = employee;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (_context.Employees == null)
            {
                TempData["error"] = "Invalid request";
                return NotFound();
            }
            var employee = await _context.Employees.FindAsync(id.ToString());

            if (employee != null)
            {
                Employee = employee;
                _context.Employees.Remove(Employee);
                await _context.SaveChangesAsync();
                TempData["success"] = "Employee deleted successfully";
            }

            return RedirectToPage("./Index");
        }
    }
}
