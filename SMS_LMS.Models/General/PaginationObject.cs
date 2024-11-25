using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Models.General
{
    public class PaginationObject<T>
    {
        public PaginationObject(List<T> dataList, int count, int pageIndex = 0, int pageCount = 0)
        {
            DataList = dataList;
            TotalCount = count;
            PageIndex = pageIndex;
            PageCount = pageCount;
        }

        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public int TotalCount { get; set; }
        public List<T> DataList { get; set; }
    }
}
