using SMS_LMS.Data.Repository.MasterRep;
using SMS_LMS.Models.DataBaseTablesModels.SystemNotificationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Data.Repository.SystemNotificationRep
{
    public interface INotificationRepository : IRepository<Notification>
    {
    }
}
