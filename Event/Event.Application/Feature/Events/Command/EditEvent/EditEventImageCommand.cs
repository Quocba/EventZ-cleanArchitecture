using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Domain.Entities.Enum;

namespace Event.Application.Feature.Events.Command.EditEvent
{
    public class EditEventImageCommand
    {
        public string ImageUrl { get; set; }
        public ImageTypeEnum ImageType { get; set; }
    }
}
