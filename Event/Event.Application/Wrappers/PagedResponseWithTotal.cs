using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.Wrappers
{
    public class PagedResponseWithTotal<T> : PagedResponse<T>
    {
        public int Total { get; set; }

        public PagedResponseWithTotal(T data, int pageNumber, int pageSize, int totalPages, int totalRecord, int total)
            : base(data, pageNumber, pageSize, totalPages, totalRecord)
        {
            Total = total;
        }
    }
}
