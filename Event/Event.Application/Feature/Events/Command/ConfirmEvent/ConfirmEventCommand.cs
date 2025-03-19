using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Domain.Entities;
using Event.Domain.Entities.Enum;
using MediatR;

namespace Event.Application.Feature.Events.Command.ConfirmEvent
{
    public class ConfirmEventCommand : IRequest
    {
        public Guid EventID { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The number of guests cannot be less than 1")]
        public int NumberOfGuest {  get; set; }
        [Required]
        public EventStatusEnum Status {  get; set; }
        [Required]
        [Range(1,double.MaxValue,ErrorMessage ="Price cannot be less than 0")]
        public double Price {  get; set; }
        public EventRegistrationLinkDTO EventRegistrationLink { get; set; }

        public ConfirmEventCommand() { }
        public ConfirmEventCommand(Guid eventID) { 
              
            EventID = eventID;
        }
    }
}
