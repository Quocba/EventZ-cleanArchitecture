using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProduct.Application.DTO;
using MediatR;

namespace EventProduct.Application.Feature.Statistical.StatisticalProductSaled
{
    public class StatisticalProductSaledQuery : IRequest<StatisticalEventProductSaledDTO>
    {
        public Guid EventID { get; set; }

        public StatisticalProductSaledQuery() { }

        public StatisticalProductSaledQuery(Guid eventID) { 
            EventID = eventID;
        }

    }
}
