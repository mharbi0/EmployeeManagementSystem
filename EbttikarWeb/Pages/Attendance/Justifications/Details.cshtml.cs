using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Data;
using EmployeeManagement.Model;

namespace EmployeeManagement.Pages.Attendance.Justifications
{
    public class DetailsModel : PageModel
    {
        private readonly EmployeeManagement.Data.ApplicationDBContext _context;

        public DetailsModel(EmployeeManagement.Data.ApplicationDBContext context)
        {
            _context = context;
        }

      public Justification Justification { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var justification = await _context.Justifications.FindAsync(id);
            if (justification == null)
            {
                return NotFound();
            }
            else 
            {
                Justification = justification;
            }
            return Page();
        }
    }
}
