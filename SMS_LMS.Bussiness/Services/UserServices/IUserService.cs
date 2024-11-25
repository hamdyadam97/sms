using SMS_LMS.Models.DataModels.ResponceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Bussiness.Services.UserServices
{
    public interface IUserService
    {
        ResponseDto GetUserData(string userId, string token);
    }
}
