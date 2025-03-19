using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Domain.Entities.Enum;
using Event.Domain.Entities.Json;
using MediatR;

namespace Event.Application.Feature.EventTimeLine.Command.EditEventTimeLine
{
    public class EditEventTimeLineCommand : IRequest
    {
        public Guid EventTimeLineID { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public HandleBy HandleBy { get; set; }

        public Guid UserID { get; set; }

        public TimeLineTypeEnum TimeLineType { get; set; }
    }
}
