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

namespace EmployeeManagement.Pages.Attendance.Justifications
{
    public class EditModel : PageModel
    {
        private readonly EmployeeManagement.Data.ApplicationDBContext _context;

        public EditModel(EmployeeManagement.Data.ApplicationDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Justification Justification { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var justification =  await _context.Justifications.FirstOrDefaultAsync(m => m.JustificationId == id);
            if (justification == null)
            {
                return NotFound();
            }
            Justification = justification;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Justification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JustificationExists(Justification.JustificationId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool JustificationExists(int id)
        {
          return _context.Justifications.Any(e => e.JustificationId == id);
        }
    }
}
