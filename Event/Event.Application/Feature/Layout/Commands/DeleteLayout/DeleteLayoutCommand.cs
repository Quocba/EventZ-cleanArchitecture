using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.Feature.Layout.Commands.DeleteLayout
{
    public class DeleteLayoutCommand : IRequest
    {
        public Guid LayoutId { get; set; }
    }
}
