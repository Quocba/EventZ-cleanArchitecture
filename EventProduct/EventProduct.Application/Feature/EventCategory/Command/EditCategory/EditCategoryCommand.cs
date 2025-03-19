using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace EventProduct.Application.Feature.EventCategory.Command.EditCategory
{
    public class EditCategoryCommand : IRequest
    {
        public Guid CategoryID { get; set; }
        public string Name {  get; set; }
        public string Description { get; set; }
    }
}
