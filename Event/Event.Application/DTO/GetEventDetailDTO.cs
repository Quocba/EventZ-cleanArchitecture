using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Event.Domain.Entities.Enum;
using Event.Domain.Entities.Json;

namespace Event.Application.DTO
{
    public class GetEventDetailDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Province { get; set; }
        public string Address { get; set; }
        public string AdditionalInfo { get; set; }
        public int NumberOfGuest { get; set; }
        public bool IsActive { get; set; }
        public bool IsOpenLayout { get; set; }
        public EventStatusEnum Status { get; set; }
        public Guid UserID { get; set; }
        public Guid EventTypeID { get; set; }
        public double Price { get; set; }
        public string EventTypeName { get; set; }
        public string EventTypeDescription { get; set; }

        public List<EventImageDTO> EventImages { get; set; } = new List<EventImageDTO>();
        public List<EventDocumentDTO> EventDocuments { get; set; } = new List<EventDocumentDTO>();
        public List<EventTimeLineDTO> EventTimeLines { get; set; } = new List<EventTimeLineDTO>();
        public List<GetEventRegistrationLinkDTO> EventRegistrationLinks { get; set; } = new List<GetEventRegistrationLinkDTO>();
    }
    public class EventDocumentDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string LinkDocument { get; set; }
        public EventDocumentsTypeEnum DocumentsType { get; set; }
    }

}
