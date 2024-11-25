using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_LMS.Models.DataModels.PagedModel
{
    // Models/PagingRequest.cs
    public class PagingRequest
    {
        private const int MaxPageSize = 100;
        private int _pageSize = 10;

        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string SortBy { get; set; } // e.g., "Name", "DateCreated"
        public bool IsDescending { get; set; } = false;

        // Additional filtering properties can be added here
    }

}
