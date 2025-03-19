using Payment.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Application.Wrappers
{
    public class PagedRequestPaymentEventHistory : PagedRequest
    {
        public Guid UserId { get; set; }
    }
}
