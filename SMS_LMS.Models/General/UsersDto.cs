using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Models.General
{
    public class UsersDto
    {
        public Guid id { get; set; }
        public string mobile { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
    }
}
