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
    public class IndexModel : PageModel
    {
        private readonly EmployeeManagement.Data.ApplicationDBContext _context;

        public IndexModel(EmployeeManagement.Data.ApplicationDBContext context)
        {
            _context = context;
        }

        public IList<Justification> Justification { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Justification = await _context.Justifications.ToListAsync();
        }
    }
}
