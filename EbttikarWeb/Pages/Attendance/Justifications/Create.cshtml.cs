using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeManagement.Data;
using EmployeeManagement.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EmployeeManagement.Pages.Attendance.Justifications
{
    public class CreateModel : PageModel
    {
        private readonly EmployeeManagement.Data.ApplicationDBContext _context;

        public CreateModel(EmployeeManagement.Data.ApplicationDBContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> OnGetAsync(string empId, DateTime checkInTime)
        //public IActionResult OnGet()
        {
            EmployeeAttendance = await _context.EmpAttendances.FindAsync(empId, checkInTime);
            if (EmployeeAttendance == null)
            {
                return NotFound();
            }

            return Page();
        }

        [BindProperty]
        public Justification Justification { get; set; }
        [BindProperty]
        public EmployeeAttendance EmployeeAttendance { get; set; }
        

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Invalid request";
                return Page();
            }

            Justification.EmployeeId = EmployeeAttendance.EmployeeId;
            Justification.CheckIn = EmployeeAttendance.CheckIn;
            Justification.DateCreated = DateTime.Now;

            _context.Justifications.Add(Justification);
            await _context.SaveChangesAsync();
            TempData["success"] = "Justification sent successfully";

            return RedirectToPage("./Index");
        }
    }
}
