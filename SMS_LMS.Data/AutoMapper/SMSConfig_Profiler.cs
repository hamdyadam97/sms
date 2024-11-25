using SMS_LMS.Models.DataBaseTablesModels;
using SMS_LMS.Models.DataModels.SMSModels.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Data.AutoMapper
{
    public partial class MapperConfig
    {
        public void SMSConfig_Profiler()
        {

            CreateMap<SMSDTO, SMSConfigTbl>()
              .ForMember(dest => dest.sURL, opt => opt.Ignore())
              .ForMember(dest => dest.pass, opt => opt.Ignore())
              .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
              .ForMember(des => des.accesskey, des => des.Ignore())
              .ForMember(dest => dest.user, opt => opt.Ignore())
              .ForMember(dest => dest.sid, opt => opt.Ignore());



        }
    }
}
