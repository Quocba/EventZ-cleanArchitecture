using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Event.Application.Feature.EventTimeLine.Command.SendMailTimeLine
{
    public class SendMailTimeLineCommand : IRequest
    {
        public Guid TimeLineID { get; set; }

    }
}
