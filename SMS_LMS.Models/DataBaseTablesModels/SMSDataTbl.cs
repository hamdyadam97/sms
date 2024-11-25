using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Models.DataBaseTablesModels
{
    public class SMSDataTbl
    {
        [Key]
        public int Id { get; set; }

        public string? MobileNo { get; set; }

        public string? Message { get; set; }

        public DateTime? SendDate { get; set; }


    }
}
