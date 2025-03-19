using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using MediatR;

namespace Event.Application.Feature.Events.Command.EditEvent
{
    public class EditEventCommand : IRequest
    {
        [Required]
        public Guid EventID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public string Province { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string AdditionalInfo { get; set; }

        [Required]
        public bool IsActive = false;

        [Required]
        public bool IsOpenLayout = false;

        [Required]
        public List<EditEventImageCommand> Images { get; set; } = new List<EditEventImageCommand>();

        [Required]
        public Guid EventTypeID { get; set; }
    }
}
