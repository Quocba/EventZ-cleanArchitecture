using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.Feature.Events;
using Event.Domain.Entities;
using Event.Application.Interfaces;

namespace Event.Application.Interface
{
    public interface IEventRepository : IRepository<Events>
    {
 
    }
}
