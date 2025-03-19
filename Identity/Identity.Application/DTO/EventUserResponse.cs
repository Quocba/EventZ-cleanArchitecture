using Identity.Domain.Entities;
using Identity.Domain.Shares;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.DTO
{
    public class EventUserResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; } = false;
        public string? Avatar { get; set; }
        public string Phone { get; set; }
        public bool PhoneConfirmed { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreateAt { get; set; }
        public int Gender { get; set; }
        public DateTime? LastModifiedAt { get; set; }

        public string EventRoleName { get; set; }
    }
}
