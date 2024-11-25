using AutoMapper;
using SMS_LMS.Models.DataBaseTablesModels;
using SMS_LMS.Models.DataModels.PleasePreDataModels.DTO;
using SMS_LMS.Models.DataModels.SMSDataModels.DTO;
using SMS_LMS.Models.DataModels.SMSModels.DTO;
using SMS_LMS.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Bussiness.AutoMapper
{
    public class SMS_LMS_Profiler : Profile
    {
        public SMS_LMS_Profiler()
        {
            CreateMap<PleasePreview, PleasePreviewDTO>().ReverseMap();
           
            //CreateMap<SMSDataTbl, SMSDataDTO>().ReverseMap();
            CreateMap<UnathorizedSMSDataDTO, SMSDataTbl>()
            .ForMember(dest => dest.MobileNo, opt => opt.MapFrom(src => src.mobiles));

            CreateMap<SMSSearchDto, SMSDataDTO>()
              .ForMember(dest => dest.MobileNo, opt => opt.MapFrom(src => src.MobileNo))
              .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message)).ReverseMap();

            CreateMap<SMSDataDTO, SMSDataTbl>()
           .ForMember(dest => dest.MobileNo, opt => opt.MapFrom(src => src.MobileNo))
           .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message))
           .ForMember(dest => dest.SendDate, opt => opt.MapFrom(src => DateTime.Now)).ReverseMap();

            CreateMap<SMSConfigTbl, SMSDTO>()
            .ForMember(dest => dest.accesskey, opt => opt.MapFrom(src => src.accesskey))
            .ForMember(dest => dest.user, opt => opt.MapFrom(src => src.user))
            .ForMember(dest => dest.pass, opt => opt.MapFrom(src => src.pass))
            .ForMember(dest => dest.sid, opt => opt.MapFrom(src => src.sid))
            .ForMember(dest => dest.sURL, opt => opt.MapFrom(src => src.sURL))
            .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.ModifiedDate)).ReverseMap();
        }
    }
}
