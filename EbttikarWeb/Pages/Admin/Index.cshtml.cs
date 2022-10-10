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
    public class IndexModel : PageModel
    {
        private readonly EmployeeManagement.Data.ApplicationDBContext _context;

        public IndexModel(EmployeeManagement.Data.ApplicationDBContext context)
        {
            _context = context;
        }

        public IList<Employee> Employee { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Employees != null)
            {
                Employee = await _context.Employees.ToListAsync();
            }
        }
    }
}
