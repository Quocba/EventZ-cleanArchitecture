using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace EventProduct.Application.Feature.EventCategory.Command.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest
    {
        public Guid CategoryID { get; set; }
        public DeleteCategoryCommand() { }
        public DeleteCategoryCommand(Guid categoryID) { CategoryID = categoryID; }
    }
}
