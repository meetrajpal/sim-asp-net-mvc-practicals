using System;
using System.Collections.Generic;

namespace Practical14.Models.ViewModels
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Records { get; set; }
        public int TotalRecords { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
    }
}