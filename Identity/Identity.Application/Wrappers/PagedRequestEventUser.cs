using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Wrappers
{
    public class PagedRequestEventUser : PagedRequestWithSearch
    {
        public Guid EventId { get; set; }
        public List<Guid> EventRoleIds { get; set; }
    }
}
