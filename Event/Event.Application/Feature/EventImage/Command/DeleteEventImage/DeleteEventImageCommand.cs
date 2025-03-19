using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Event.Application.Feature.EventImage.Command.DeleteEventImage
{
    public class DeleteEventImageCommand : IRequest
    {
        public Guid EventImageID { get; set; }
    }
}
