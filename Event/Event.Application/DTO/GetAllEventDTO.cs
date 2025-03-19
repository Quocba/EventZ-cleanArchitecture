using Event.Application.DTO;
using Event.Domain.Entities.Enum;
using Event.Domain.Shares;
using Newtonsoft.Json;

public class GetAllEventDTO
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
    public string Status { get; set; }
    public double Price {  get; set; }
    public Guid UserID { get; set; }
    public Guid EventTypeID { get; set; }
    public string EventTypeName { get; set; }
    public string EventTypeDescription { get; set; }

    public List<EventImageDTO> EventImages { get; set; } = new List<EventImageDTO>();

}
