using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Models.DataBaseTablesModels
{
    public class ApplicationRole : IdentityRole
    {

        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
    }
}
