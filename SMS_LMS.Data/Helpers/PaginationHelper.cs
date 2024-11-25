using SMS_LMS.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Data.Helpers
{
    public class PaginationHelper
    {
        public static PaginationObject<T> Create<T>(IQueryable<T> query, int pageIndex = 1, int pageSize = 100)
        {
            int records = query.Count();
            if (records <= pageSize || pageIndex <= 0) pageIndex = 1;
            int pages = (int)Math.Ceiling((double)records / pageSize);
            int excludedRows = (pageIndex - 1) * pageSize;
            var items = query.Skip(excludedRows).Take(pageSize).ToList();
            return new PaginationObject<T>(items, records, pageIndex, pages);
        }
    }
}
