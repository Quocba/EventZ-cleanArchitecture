using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Domain.Entities.Enum;
using Event.Domain.Entities.Json;
using MediatR;

namespace Event.Application.Feature.EventTimeLine.Command.AddEventTimeLine
{
    public class AddEventTimeLineCommand : IRequest<Guid>
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime Start_Date { get; set; }

        [Required]
        public DateTime End_Date { get; set; }

        [Required]
        public HandleBy HandleBy { get; set; }

        [Required]
        public TimeLineTypeEnum TimeLineType { get; set; }

        [Required]
        public EventTimeLineStatusEnum Status { get; set; }

        [Required]
        public Guid ParentID { get; set; }

        [Required]
        public Guid EventID { get; set; }
    }
}
