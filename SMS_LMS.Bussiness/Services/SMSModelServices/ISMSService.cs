using SMS_LMS.Models.DataModels.ResponceModel;
using SMS_LMS.Models.DataModels.SMSDataModels.DTO;
using SMS_LMS.Models.DataModels.SMSModels.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Bussiness.Services.SMSModelServices
{
    public interface ISMSService
    {

        ResponseDto GetAll();
        Task<ResponseDto> SendSMS(SMSDataDTO Entity);
       
        Task<ResponseDto> GetData();
      
        Task<ResponseDto> Add(SMSDTO Entity);
        Task<ResponseDto> SendSMS(UnathorizedSMSDataDTO Entity);
       


    }
}
