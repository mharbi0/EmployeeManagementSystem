using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Data;
using EmployeeManagement.Model;

namespace EmployeeManagement.Pages.Attendance
{
    public class DetailsModel : PageModel
    {
        private readonly EmployeeManagement.Data.ApplicationDBContext _context;

        public DetailsModel(EmployeeManagement.Data.ApplicationDBContext context)
        {
            _context = context;
        }
        public Model.EmployeeAttendance EmployeeAttendance { get; set; }

        public async Task<IActionResult> OnGetAsync(DateTime checkIn, string employeeId)
        {
            var employeeattendance = await _context.EmpAttendances.FindAsync(employeeId, checkIn);
            if (employeeattendance == null)
            {
                return NotFound();
            }
            else
            {
                EmployeeAttendance = employeeattendance;
            }
            return Page();
        }
    }
}
