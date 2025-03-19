using Event.Domain.Entities.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Event.Application.Feature.Layout.Commands.UpdateLayout
{
    public class UpdateLayoutCommand : IRequest
    {
        public string? LayoutName { get; set; }

        public int? LayoutFloorNumber { get; set; }

        public LayoutTypeEnum? LayoutType { get; set; }

        public int? Rows { get; set; }

        public int? Cols { get; set; }

        [JsonIgnore]
        public Guid Id { get; set; }
    }
}
