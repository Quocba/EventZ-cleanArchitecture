using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Domain.Shares;
using MediatR;

namespace Event.Application.Feature.EventInvite.Command
{
    public class InviteUserCommand : IRequest<Guid>
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsChecked { get; set; } = false;
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime CreateAt { get; set; } = DateUtility.GetCurrentDateTime();
        public DateTime UpdateAt { get; set; } = DateUtility.GetCurrentDateTime();
        public Guid EventID { get; set; }
    }
}
