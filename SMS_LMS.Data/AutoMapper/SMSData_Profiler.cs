using SMS_LMS.Models.DataBaseTablesModels;
using SMS_LMS.Models.DataModels.SMSDataModels.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Data.AutoMapper
{
    public partial class MapperConfig
    {
        public void SMSData_Profiler()
        {
            CreateMap<SMSDataDTO, SMSDataTbl>().ReverseMap();
          

        }
    }
}
