using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Event.Application.Feature.EventTimeLine.Command.DeleteEventTimeLine
{
    public class DeleteEventTimeLineCommand : IRequest
    {
        public Guid EventTimeLineID { get; set; }
    }
}
