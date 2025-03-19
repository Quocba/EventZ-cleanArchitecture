using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Application.Wrappers
{
    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int TotalPage { get; set; }
        public int PageSize { get; set; }
        public int TotalRecord { get; set; }

        public PagedResponse(T data, int pageNumber, int pageSize, int totalPage, int totalRecord)
        {
            PageNumber = pageNumber;
            TotalPage = totalPage;
            PageSize = pageSize;
            Data = data;
            TotalRecord = totalRecord;
        }
    }
}
