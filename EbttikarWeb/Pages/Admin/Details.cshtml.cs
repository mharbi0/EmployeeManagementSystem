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
    public class DetailsModel : PageModel
    {
        private readonly EmployeeManagement.Data.ApplicationDBContext _context;

        public DetailsModel(EmployeeManagement.Data.ApplicationDBContext context)
        {
            _context = context;
        }

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
    }
}
