using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Domain.Entities.Enum
{
    public enum EventPackageRegistrationStatusEnum
    {
        Pending = 1,      
        Approved = 2,     
        Rejected = 3, 
        Cancelled = 4,    
        Completed = 5     
    }
}
