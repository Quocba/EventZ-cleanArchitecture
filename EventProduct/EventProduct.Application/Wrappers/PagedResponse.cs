using System;
using System.Collections.Generic;
using System.Text;

namespace EventProduct.Application.Wrappers
{
    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int TotalPage { get; set; }
        public int PageSize { get; set; }
        public int TotalRecord { get; set; }

        public PagedResponse(T data, int pageNumber, int pageSize, int totalPage, int totalRecord)
        {
            this.PageNumber = pageNumber;
            this.TotalPage = totalPage;
            this.PageSize = pageSize;
            this.Data = data;
            this.TotalRecord = totalRecord;
        }
    }
}
