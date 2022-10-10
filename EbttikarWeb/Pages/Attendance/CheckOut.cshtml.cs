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

namespace EmployeeManagement.Pages.Attendance
{
    public class EditModel : PageModel
    {
        private readonly EmployeeManagement.Data.ApplicationDBContext _context;

        public EditModel(EmployeeManagement.Data.ApplicationDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Model.EmployeeAttendance EmployeeAttendance { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(DateTime checkIn, string employeeId)
        {
            if (employeeId == null || _context.EmpAttendances == null)
            {
                return NotFound();
            }

            var employeeattendance =  await _context.EmpAttendances.FindAsync(employeeId, checkIn);
            if (employeeattendance == null)
            {
                return NotFound();
            }
            EmployeeAttendance = employeeattendance;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            DateTime dateTime = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            //1: CheckOut is older than 1 minute
            //2: CheckOut is in the future
            if (DateTime.Compare((DateTime)EmployeeAttendance.CheckOut, dateTime.AddMinutes(-1)) < 0
                || DateTime.Compare((DateTime)EmployeeAttendance.CheckOut, dateTime) > 0)
            {
                TempData["error"] = "The page was open for longer than one minute";
                return Page();
            }

            if (((DateTime) EmployeeAttendance.CheckOut).Hour < 15)
            {
                EmployeeAttendance.EarlyCheckOut = true;
                TempData["warning"] = "Early check out recorded";
            }

            _context.Attach(EmployeeAttendance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                TempData["success"] = "Checked out successfully";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeAttendanceExists(EmployeeAttendance.CheckIn))
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

        private bool EmployeeAttendanceExists(DateTime id)
        {
          return _context.EmpAttendances.Any(e => e.CheckIn == id);
        }
    }
}
