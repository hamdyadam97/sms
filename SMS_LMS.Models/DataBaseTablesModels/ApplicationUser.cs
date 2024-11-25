using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Models.DataBaseTablesModels
{
    public class ApplicationUser : IdentityUser
    {
        // add identity property
        public string? FullName { get; set; }
        public string? FullNameAr { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? CreatedDeate { get; set; } = DateTime.Now;
        public bool? IsActive { get; set; } = true;
        public bool? IsDeleted { get; set; } = false;
    }
}
