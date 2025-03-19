using Identity.Domain.Shares;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.DTO
{
    public class GetMeResponse
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; } = false;
        public string? Avatar { get; set; }
        public string Phone { get; set; }
        public bool IsPhoneConfirmed { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public DateTime CreateAt { get; set; } = DateUtility.GetCurrentDateTime();
        public int Gender { get; set; }
        public List<string> Roles { get; set; }
    }
}
