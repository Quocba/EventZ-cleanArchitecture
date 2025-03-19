using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string Avatar { get; set; }
        public string Phone { get; set; }
        public bool PhoneConfirmed { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateAt { get; set; }
        public int Gender { get; set; }
        public string UserCode { get; set; }
        public DateTime LastModifiedAt { get; set; }
        public List<string> Roles { get; set; }
    }
}
