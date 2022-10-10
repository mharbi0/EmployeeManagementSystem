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
    public class IndexModel : PageModel
    {
        private readonly EmployeeManagement.Data.ApplicationDBContext _context;

        public IndexModel(EmployeeManagement.Data.ApplicationDBContext context)
        {
            _context = context;
        }

        public IList<Model.EmployeeAttendance> EmployeeAttendance { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.EmpAttendances != null)
            {
                EmployeeAttendance = await _context.EmpAttendances.ToListAsync();
            }
        }
    }
}
