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
using Microsoft.AspNetCore.Identity;
using System.Dynamic;

namespace EmployeeManagement.Pages.Attendance
{
    public class CreateModel : PageModel
    {
        private readonly EmployeeManagement.Data.ApplicationDBContext _context;
        private readonly UserManager<Employee> _userManager;

        public CreateModel(EmployeeManagement.Data.ApplicationDBContext context, UserManager<Employee> userManager)
        {
            _context = context;
            _userManager = userManager;
            EmployeeAttendance = new EmployeeAttendance();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();
            var userId = user?.Id;
            EmployeeAttendance.EmployeeId = userId;
            return Page();
        }

        [BindProperty]
        public Model.EmployeeAttendance EmployeeAttendance { get; set; }
        
        public async Task<IActionResult> OnPostAsync()
        {   
            DateTime dateTime = DateTime.Now;

            //1: CheckIn is older than 1 minute
            //2: CheckIn is in the future
            if (DateTime.Compare(EmployeeAttendance.CheckIn, dateTime.AddMinutes(-1)) < 0
                || DateTime.Compare(EmployeeAttendance.CheckIn, dateTime) > 0)
            {
                TempData["error"] = "The page was open for longer than one minute";
                return Page();
            }

            var employee = await _context.Employees.FindAsync(EmployeeAttendance.EmployeeId);
            if (employee == null)
            {
                ModelState.AddModelError(string.Empty, "Employee ID not found. Contact support.");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                TempData["error"] = "Invalid request";
                return Page();
            }

            if (EmployeeAttendance.CheckIn.Hour < 7)
            {   // Checking in before 7 AM is not allowed
                TempData["error"] = "Checking in starts at 7AM";
                return Page();
            }

            if (EmployeeAttendance.CheckIn.Hour > 8
                || (EmployeeAttendance.CheckIn.Hour == 8 && EmployeeAttendance.CheckIn.Minute > 29))
            {   // Checking in after 8:30 AM should be flagged with LateCheckIn
                EmployeeAttendance.LateCheckIn = true;
                TempData["warning"] = "Late check in recorded";
            }

            _context.EmpAttendances.Add(EmployeeAttendance);
            await _context.SaveChangesAsync();
            TempData["success"] = "Checked in successfully";

            return RedirectToPage("./Index");
        }
    }
}
