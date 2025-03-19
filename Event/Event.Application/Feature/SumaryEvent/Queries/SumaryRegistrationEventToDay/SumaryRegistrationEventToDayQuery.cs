﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Application.Wrappers;
using MediatR;

namespace Event.Application.Feature.SumaryEvent.Queries.SumaryRegistrationEventToDay
{
    public class SumaryRegistrationEventToDayQuery : IRequest<PagedResponse<List<GetEventPackageRegistrationDTO>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
