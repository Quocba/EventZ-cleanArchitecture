using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace EventProduct.Application.Feature.EventCategory.Command.AddCategory
{
    public class AddCategoryCommand : IRequest<Guid>
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid EventID { get; set; }

    }
}
