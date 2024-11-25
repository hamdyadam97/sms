using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Models.DataModels.SMSModels.DTO
{
    public class SMSDTO
    {
        public string? accesskey { get; set; }
        public string? user { get; set; }

        public string pass { get; set; }
        public string sid { get; set; }
        public string sURL { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
