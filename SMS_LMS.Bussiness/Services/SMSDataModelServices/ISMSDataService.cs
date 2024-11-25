using SMS_LMS.Models.DataBaseTablesModels;
using SMS_LMS.Models.DataModels.ResponceModel;
using SMS_LMS.Models.DataModels.SMSDataModels.DTO;
using SMS_LMS.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Bussiness.Services.SMSDataModelServices
{
    public interface ISMSDataService
    {
        Task<ResponseDto> Add(SMSDataDTO Entity);
        ResponseDto GetAll();
        Task<ResponseDto> Getlist(int pageNum, int pagSize, Expression<Func<SMSDataTbl, object>> orderBy = null, string orderByDirection = "ASC");
        Task<ResponseDto> GetAll(SMSSearchDto searchCriteria);
    }
}
