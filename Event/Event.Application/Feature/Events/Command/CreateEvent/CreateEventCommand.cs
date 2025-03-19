using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Domain.Entities;
using Event.Domain.Entities.Enum;
using MediatR;

namespace Event.Application.Feature.Events.Command.CreateEvent
{
    public class CreateEventCommand : IRequest<Guid>
    {
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
        [Range(0, int.MaxValue, ErrorMessage = "Number of guests must be at least 0.")]
        public int NumberOfGuest { get; } = 0;

        [Required]
        public bool IsActive = false;

        [Required]
        public bool IsOpenLayout = false;

        [Required]
        public EventStatusEnum Status = EventStatusEnum.WAIT_FOR_APPROVAL;

        [Required]
        public List<CreateEventImageCommand> Images { get; set; } = new List<CreateEventImageCommand>();

        [Required]
        public Guid UserID { get; set; }

        [Required]
        public Guid EventTypeID { get; set; }


    }
}
