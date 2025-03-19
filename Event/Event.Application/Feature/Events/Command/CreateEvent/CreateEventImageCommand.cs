using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Domain.Entities.Enum;

namespace Event.Application.Feature.Events.Command.CreateEvent
{
    public class CreateEventImageCommand
    {
        public string ImageUrl { get; set; }
        public ImageTypeEnum ImageType { get; set; }
    }
}
