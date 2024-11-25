using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Models.General
{
    public enum ResponseMessagesEnum
    {
        OK = 200,
        UnAuthorized = 401,
        NoContent = 201,
        InternalError = 500,

    }
}
