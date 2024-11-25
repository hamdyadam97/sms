using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Models.DataModels.SMSDataModels.DTO
{
    public class SMSDataDTO
    {
        public string? MobileNo { get; set; } = null;
        public string? Message { get; set; }
    }
    public class UnathorizedSMSDataDTO
    {
        public string? mobiles { get; set; } = null;
        public string Message { get; set; }
    }
}
