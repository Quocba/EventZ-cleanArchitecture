﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.DTO;
using EventProduct.Application.Wrappers;
using MediatR;

namespace EventProduct.Application.Feature.EventOrder.Queries.GetEventOrderByPayment
{
    public class GetEventOrderByUserQuery : IRequest<PagedResponse<List<EventOrderDTO>>>
    {
        public int PageNumber = 1;
        public int PageSize = 10;
        public Guid UserID { get; set; }
        
        public GetEventOrderByUserQuery() { }

        public GetEventOrderByUserQuery(Guid userID)
        {
            UserID = userID;
        }
    }
}
