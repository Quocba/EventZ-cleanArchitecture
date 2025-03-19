using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.Wrappers;
using MediatR;

namespace Event.Application.Feature.Events.Queries.GetEventByUser
{
    public class GetEventByUserQuery : IRequest<PagedResponse<List<GetAllEventDTO>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        [Required(ErrorMessage = "UserID Can be not empty")]
        public Guid UserID { get; set; }

        public GetEventByUserQuery() { }
        public GetEventByUserQuery(Guid userID) { 
            
            UserID = userID;
        }
    }
}
