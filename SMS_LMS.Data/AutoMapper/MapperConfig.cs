using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Data.AutoMapper
{
    public partial class MapperConfig : Profile
    {
        public MapperConfig()
        {
            SMSConfig_Profiler();
            SMSData_Profiler();


        }
    }
}
