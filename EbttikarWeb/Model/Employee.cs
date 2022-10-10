using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Model
{
    public class Employee : IdentityUser
    {
        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Display(Name = "Administrator")]
        public bool Admin { get; set; } = false;

        public bool Active { get; set; } = true;
    }
}
