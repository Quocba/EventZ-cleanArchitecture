using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Domain.Entities.Enum;
using Event.Domain.Shares;
using MediatR;

namespace Event.Application.Feature.EventPackageRegistration.RegistrationEventPackge
{
    public class RegistrationEventPackageCommand : IRequest
    {
        [Required]
        public string Name {  get; set; }

        [Required]
        public string Email {  get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public DateTime CreateAt {  get; set; } = DateUtility.GetCurrentDateTime();

        [Required]
        public DateTime UpdateAt { get; set; } = DateUtility.GetCurrentDateTime();

        [Required]
        public string UpdateBy {  get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Price must be at least 1.")]
        public double Price = 0;

        [Required]
        public Guid EventPackageID { get; set;}


    }
}
