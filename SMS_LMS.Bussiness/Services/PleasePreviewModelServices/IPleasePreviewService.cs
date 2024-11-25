using SMS_LMS.Models.DataModels.PleasePreDataModels.DTO;
using SMS_LMS.Models.DataModels.ResponceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Bussiness.Services.PleasePreviewModelServices
{
    public interface IPleasePreviewService
    {
        ResponseDto Add(PleasePreviewDTO Entity);
        ResponseDto GetAll();
    }
}
