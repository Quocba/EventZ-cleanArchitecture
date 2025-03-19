using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EventProduct.Application.DTO
{
    public class GetUserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; } = false;
        public string Phone { get; set; }
        public bool PhoneConfirmed { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreateAt { get; set; }
        public int Gender { get; set; }
        public string UserCode { get; set; }
        public DateTime? LastModifiedAt { get; set; }



    }
}
