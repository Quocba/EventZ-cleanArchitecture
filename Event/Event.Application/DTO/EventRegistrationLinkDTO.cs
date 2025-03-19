using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.DTO
{
    public class EventRegistrationLinkDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
