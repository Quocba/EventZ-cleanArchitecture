using Event.Domain.Entities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using MediatR;

namespace Event.Application.Feature.Layout.Commands.CreateLayout
{
    public class CreateLayoutCommand : IRequest<Guid>
    {
        [MaxLength(255)]
        [Required]
        public string LayoutName { get; set; }

        [Required]
        public int LayoutFloorNumber { get; set; }

        [Required]
        public LayoutTypeEnum LayoutType { get; set; }

        [Required]
        public int Rows { get; set; }

        [Required]
        public int Cols { get; set; }

        [JsonIgnore]
        public string CreatedBy { get; set; }
    }
}
