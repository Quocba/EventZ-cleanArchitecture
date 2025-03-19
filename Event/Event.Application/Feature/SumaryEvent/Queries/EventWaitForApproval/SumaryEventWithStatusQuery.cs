using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Application.Wrappers;
using MediatR;

namespace Event.Application.Feature.SumaryEvent.Queries.EventWaitForApproval
{
    public class SumaryEventWithStatusQuery : IRequest<GetEventByStatusDTO>
    {
        public int status {  get; set; }
    }
}
