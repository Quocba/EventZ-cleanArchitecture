using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Event.Application.DTO;
using Event.Application.Interface;
using Event.Application.Wrappers;
using Event.Domain.Entities;
using Event.Domain.Entities.Enum;
using Event.Domain.Entities.Json;
using Event.Infrastructure.Context;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Event.Infrastructure.Queries
{
    public class EventQuery(ISqlConnectionFactory _connectFactory, IConfiguration _configuration) : IEventQuery
    {
        public async Task<int> CountEventCreated(Guid? userId)
        {
            using var connection = _connectFactory.GetOpenConnection();

            const string countCreatedQuery = @"
                                    SELECT COUNT(*) 
                                    FROM events 
                                    WHERE (@UserId IS NULL OR user_id = @UserId)"
            ;

            if (userId == Guid.Empty) userId = null;

            int totalCreated = await connection.ExecuteScalarAsync<int>(countCreatedQuery, new { UserId = userId });

            return totalCreated;
        }

        public async Task<PagedResponse<List<GetAllEventDTO>>> GetAllEvent(PagedRequestWithSearch request, string? status)
        {
            using var connection = _connectFactory.GetOpenConnection();


            const string sqlQuery = @"
                                    WITH event_ids AS (
                                        SELECT id
                                        FROM events
                                        WHERE (@Status IS NULL OR status = @Status)
                                        ORDER BY id
                                        OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY
                                    )
                                    SELECT 
                                        e.id AS Id, 
                                        e.title AS Title, 
                                        e.description AS Description, 
                                        e.start_time AS StartTime, 
                                        e.end_time AS EndTime, 
                                        e.province AS Province,     
                                        e.address AS Address, 
                                        e.additional_info AS AdditionalInfo, 
                                        e.number_of_guest AS NumberOfGuest, 
                                        e.is_active AS IsActive, 
                                        e.is_open_layout AS IsOpenLayout, 
                                        e.status AS Status,
                                        e.price AS Price,
                                        e.user_id AS UserID, 
                                        e.event_type_id AS EventTypeID, 
                                        et.name AS EventTypeName, 
                                        et.description AS EventTypeDescription,
                                        COALESCE(ei.image_url, '') AS ImageUrl, 
                                        COALESCE(ei.image_type, '') AS ImageType
                                    FROM event_ids ei_ids
                                    INNER JOIN events e ON ei_ids.id = e.id
                                    LEFT JOIN event_type et ON e.event_type_id = et.id
                                    LEFT JOIN event_images ei ON e.id = ei.event_id;";

            var offset = (request.PageNumber - 1) * request.PageSize;

            var eventDictionary = new Dictionary<Guid, GetAllEventDTO>();

            var result = await connection.QueryAsync<GetAllEventDTO, EventImageDTO, GetAllEventDTO>(
                sqlQuery,
                (eventDto, eventImage) =>
                {
                    if (!eventDictionary.TryGetValue(eventDto.Id, out var existingEvent))
                    {
                        existingEvent = eventDto;
                        existingEvent.EventImages = new List<EventImageDTO>();
                        eventDictionary.Add(existingEvent.Id, existingEvent);
                    }
                    if (eventImage != null && !string.IsNullOrEmpty(eventImage.ImageUrl))
                    {
                        existingEvent.EventImages.Add(eventImage);
                    }
                    return existingEvent;
                },
                param: new { Status = status ?? (object)DBNull.Value, Offset = offset, PageSize = request.PageSize },
                splitOn: "ImageUrl"
            );

            const string countQuery = @"
                                    SELECT COUNT(*) 
                                    FROM events 
                                    WHERE (@Status IS NULL OR Status = @Status)";

            int totalRecords = await connection.ExecuteScalarAsync<int>(countQuery, new { Status = status ?? (object)DBNull.Value });

            return new PagedResponse<List<GetAllEventDTO>>(
                eventDictionary.Values.ToList(),
                request.PageNumber,
                request.PageSize,
                (int)Math.Ceiling((double)totalRecords / request.PageSize),
                totalRecords
            );
        }

        public async Task<PagedResponse<List<GetAllEventDTO>>> GetEventByUser(PagedRequest request, Guid userID)
        {
            using var connection = _connectFactory.GetOpenConnection();

            const string sqlQuery = @"   
                                WITH event_ids AS (
                                    SELECT id 
                                    FROM events 
                                    WHERE user_id = @UserID
                                    ORDER BY id
                                    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY
                                )
                                SELECT 
                                e.id AS Id, 
                                e.title AS Title, 
                                e.description AS Description, 
                                e.start_time AS StartTime, 
                                e.end_time AS EndTime, 
                                e.province AS Province, 
                                e.address AS Address, 
                                e.additional_info AS AdditionalInfo, 
                                e.number_of_guest AS NumberOfGuest, 
                                e.is_active AS IsActive, 
                                e.is_open_layout AS IsOpenLayout, 
                                e.status AS Status,
                                e.price AS Price,
                                e.user_id AS UserID, 
                                e.event_type_id AS EventTypeID, 
                                et.name AS EventTypeName, 
                                et.description AS EventTypeDescription,
                                ei.image_url AS ImageUrl, 
                                ei.image_type AS ImageType,
                                ei.event_id AS EventID
                            FROM event_ids ei_ids
                            INNER JOIN events e ON ei_ids.id = e.id
                            LEFT JOIN event_type et ON e.event_type_id = et.id
                            LEFT JOIN event_images ei ON e.id = ei.event_id;";

            var offset = (request.PageNumber - 1) * request.PageSize;
            var eventDictionary = new Dictionary<Guid, GetAllEventDTO>();

            var result = await connection.QueryAsync<GetAllEventDTO, EventImageDTO, GetAllEventDTO>(
              sqlQuery,
              (eventDto, eventImage) =>
              {
                  if (!eventDictionary.TryGetValue(eventDto.Id, out var existingEvent))
                  {
                      existingEvent = eventDto;
                      existingEvent.EventImages = new List<EventImageDTO>();
                      eventDictionary.Add(existingEvent.Id, existingEvent);
                  }
                  if (eventImage != null && !string.IsNullOrEmpty(eventImage.ImageUrl))
                  {
                      existingEvent.EventImages.Add(eventImage);
                  }

                  return existingEvent;
              },
              param: new { UserID = userID, Offset = offset, PageSize = request.PageSize },
              splitOn: "ImageUrl"
            );

            const string sqlCount = "SELECT COUNT(DISTINCT id) FROM events WHERE user_id = @UserID";
            var totalRecords = await connection.ExecuteScalarAsync<int>(sqlCount, new { UserID = userID });

            return new PagedResponse<List<GetAllEventDTO>>(
                eventDictionary.Values.ToList(),
                request.PageNumber,
                request.PageSize,
                (int)Math.Ceiling((double)totalRecords / request.PageSize),
                totalRecords
            );
        }



        public async Task<GetEventDetailDTO> GetEventDetail(Guid eventID)
        {
                using var connection = _connectFactory.GetOpenConnection();
          
                const string sqlQuery = @"
            SELECT e.id AS Id, e.title AS Title, e.description AS Description, 
                   e.start_time AS StartTime, e.end_time AS EndTime,
                   e.province AS Province, e.address AS Address, 
                   e.additional_info AS AdditionalInfo, e.number_of_guest AS NumberOfGuest,
                   e.is_active AS IsActive, e.is_open_layout AS IsOpenLayout, e.status AS Status,
                   e.user_id AS UserID, e.event_type_id AS EventTypeID, e.price AS Price,
                   et.name AS EventTypeName, et.description AS EventTypeDescription
            FROM events e
            LEFT JOIN event_type et ON e.event_type_id = et.id
            WHERE e.id = @EventID;

            SELECT ei.image_url AS ImageUrl, ei.image_type AS ImageType, ei.event_id AS EventID
            FROM event_images ei
            WHERE ei.event_id = @EventID;

            SELECT ed.id AS Id, ed.title AS Title, ed.content AS Content, 
                   ed.link_document AS LinkDocument, ed.documents_type AS DocumentsType
            FROM event_documents ed
            WHERE ed.event_id = @EventID;

            SELECT etl.title AS Title, etl.content AS Content, etl.start_date AS Start_Date,
                   etl.end_date AS End_Date, etl.handle_by AS HandleBy,
                   etl.timeline_type AS TimeLineType, etl.parent_id AS ParentID, 
                   etl.status AS Status, etl.event_id AS EventID
            FROM event_timeline etl
            WHERE etl.event_id = @EventID;

            SELECT erl.code AS Code, erl.start_date AS StartDate, erl.end_date AS EndDate
            FROM event_registration_link erl
            WHERE erl.event_id = @EventID;
        ";
            using var multi = await connection.QueryMultipleAsync(sqlQuery, new { EventID = eventID });

            var eventDetail = multi.Read<GetEventDetailDTO>().FirstOrDefault();
            if (eventDetail == null) return null;

            eventDetail.EventImages = multi.Read<EventImageDTO>().ToList();
            eventDetail.EventDocuments = multi.Read<EventDocumentDTO>().ToList();
            eventDetail.EventTimeLines = multi.Read<(string Title, string Content, DateTime StartDate, DateTime EndDate, string HandleBy, string TimeLineType, Guid ParentID, string Status, Guid EventID)>()
                .Select(row => new EventTimeLineDTO
                {
                    Title = row.Title,
                    Content = row.Content,
                    StartDate = row.StartDate,
                    EndDate = row.EndDate,
                    HandleBy = string.IsNullOrWhiteSpace(row.HandleBy)
                        ? new HandleBy()
                        : System.Text.Json.JsonSerializer.Deserialize<HandleBy>(row.HandleBy),
                    TimeLineType = Enum.TryParse(row.TimeLineType, out TimeLineTypeEnum type) ? type : default,
                    Status = Enum.TryParse(row.Status, out EventTimeLineStatusEnum status) ? status : default,
                    ParentID = row.ParentID,
                    EventID = row.EventID
                }).ToList();
            eventDetail.EventRegistrationLinks = multi
         .Read<(string Code, DateTime StartDate, DateTime EndDate)>()
         .Select(row => new GetEventRegistrationLinkDTO
         {
             Code = _configuration["Hosting:Host"] + "/" + row.Code,
             StartDate = row.StartDate,
             EndDate = row.EndDate
         })
         .ToList();

            return eventDetail;
        }

        public async Task<List<EventDTO>> GetUpComingEvent()
        {
            using var connection = _connectFactory.GetOpenConnection();
            const string sqlQuery = @"
                                    SELECT 
                                        id AS Id, 
                                        title AS Title, 
                                        description AS Description, 
                                        start_time AS StartTime, 
                                        end_time AS EndTime, 
                                        province AS Province,     
                                        address AS Address, 
                                        additional_info AS AdditionalInfo, 
                                        number_of_guest AS NumberOfGuest, 
                                        is_active AS IsActive,  
                                        is_open_layout AS IsOpenLayout, 
                                        status AS Status,
                                        price AS Price
                                    FROM events
                                    WHERE DATEDIFF(DAY, GETDATE(), start_time) <= 3";

            var listEvent = await connection.QueryAsync<EventDTO>(sqlQuery);

            return listEvent.ToList();
        }

        public async Task<int> SumaryEventWithStatus(int status)
        {
            using var connection = _connectFactory.GetOpenConnection();
            const string sqlQuery = @"SELECT COUNT (id)
                                    FROM events
                                    WHERE status = @status";
            var paramenter = new
            {
                Status = status
            };

            int totalEvent = await connection.ExecuteScalarAsync<int>(sqlQuery, paramenter);   

           return totalEvent;

        }
    }
}
