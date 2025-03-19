using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Event.Application.Feature.EventPackageRegistration.Commands.EditEventPackageRegistrationStatus
{
    public class EditEventPackageRegistrationStatusCommand : IRequest
    {
        [Required]
        public Guid EventPackageRegistrationID { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public double Price {  get; set; }
    }
}
