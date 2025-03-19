using Event.Domain.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Application.DTO
{
    public class LayoutResponse
    {
        public Guid Id { get; set; }

        public string? LayoutName { get; set; }

        public int? LayoutFloorNumber { get; set; }

        public LayoutTypeEnum? LayoutType { get; set; }

        public int? Rows { get; set; }

        public int? Cols { get; set; }

        public string createdBy { get; set; }
    }
}
