using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Models.General
{
    public class SMSSearchDto
    {
        public PageInfoDTO PageInfoDto { get; set; }
        public string? MobileNo { get; set; }
        public string? Message { get; set; }
        public DateTime? SendDate { get; set; }
        public DateTime? SendDateEnd { get; set; }
        public string? SearchTerm { get; set; }
    }
}
