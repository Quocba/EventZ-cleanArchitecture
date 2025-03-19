using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventProduct.Application.DTO
{
    public class GetEventDetailDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Province { get; set; }
        public string Address { get; set; }
        public string AdditionalInfo { get; set; }
        public bool IsDeleted { get; set; }
        public string eventType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string Avatar { get; set; }
        public string Phone { get; set; }
        public bool IsPhoneConfirmed { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Gender { get; set; }

    }
}
