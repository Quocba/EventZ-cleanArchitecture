using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Event.Application.DTO;
using Event.Application.Interface;
using Event.Application.Wrappers;
using Event.Domain.Entities.Enum;
using Event.Domain.Entities.Json;
using Event.Infrastructure.Context;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace Event.Infrastructure.Queries
{
    public class EventTimeLineQuery(ISqlConnectionFactory _sqlConnectionFactory) : IEventTimeLineQuery
    {
        public async Task<PagedResponse<List<GetHandleByEventTimeLineDTO>>> GetHandleByEventTimeLine(PagedRequest request)
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();
            const string sqlQuery = @"
                                    SELECT
                                        id AS TimeLineID,
                                        title AS TimeLineTitle, 
                                        content AS Content, 
                                        start_date AS TimeLineStartDate, 
                                        end_date AS TimeLineEndDate, 
                                        handle_by AS HandleBy,
                                        timeline_type AS TimeLineType
                                    FROM [EventZ_EventDataBase].[dbo].[event_timeline]
                                    ORDER BY id
                                    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

                                    SELECT COUNT(id) FROM event_timeline;
                                ";

            var parameters = new
            {
                request.PageSize,
                Offset = (request.PageNumber - 1) * request.PageSize,
            };

            using var multi = await connection.QueryMultipleAsync(sqlQuery, parameters);
            var eventTimeLines = (await multi.ReadAsync<dynamic>()).ToList();
            var totalRecords = await multi.ReadSingleAsync<int>();

            var mappedEventTimeLines = eventTimeLines.Select(row => new GetHandleByEventTimeLineDTO
            {
                TimeLineID = row.TimeLineID,
                TimeLineTitle = row.TimeLineTitle,
                Content = row.Content,
                TimeLineStartDate = row.TimeLineStartDate,
                TimeLineEndDate = row.TimeLineEndDate,
                TimeLineType = (TimeLineTypeEnum)row.TimeLineType,
                HandleBy = !string.IsNullOrEmpty(Convert.ToString(row.HandleBy))
                    ? JsonConvert.DeserializeObject<HandleBy>(Convert.ToString(row.HandleBy)!)
                    : new HandleBy()
            }).ToList();

            return new PagedResponse<List<GetHandleByEventTimeLineDTO>>(
                mappedEventTimeLines,
                request.PageNumber,
                request.PageSize,
                (int)Math.Ceiling((double)totalRecords / request.PageSize),
                totalRecords
            );
        }

        public async Task<PagedResponse<List<EventTimeLineDTO>>> GetTimeLineByEvent(PagedRequest request, Guid eventID, string? status)
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();
            const string sqlQuery = @"
                                    WITH timeline_ids AS (
                                        SELECT id
                                        FROM event_timeline
                                        WHERE event_id = @eventID
                                        AND (@status IS NULL OR status = @status)
                                        ORDER BY start_date
                                        OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY
                                    )
                                    SELECT
                                        et.title AS Title,
                                        et.content AS Content,
                                        et.start_date AS StartDate,
                                        et.end_date AS EndDate,
                                        et.handle_by AS HandleBy,
                                        et.timeline_type AS TimeLineType,
                                        et.parent_id AS ParentID,
                                        et.status AS Status
                                    FROM timeline_ids tid
                                    INNER JOIN event_timeline et ON tid.id = et.id;";

            var offset = (request.PageNumber - 1) * request.PageSize;

            EventTimeLineStatusEnum? statusEnum = null;
            if (!string.IsNullOrEmpty(status) && Enum.TryParse(status, out EventTimeLineStatusEnum parsedStatus))
            {
                statusEnum = parsedStatus;
            }

            var result = await connection.QueryAsync<dynamic>(sqlQuery, new { eventID, status = statusEnum, Offset = offset, PageSize = request.PageSize });

            var timeLineList = result.Select(row => new EventTimeLineDTO
            {
                Title = row.Title,
                Content = row.Content,
                StartDate = row.StartDate,
                EndDate = row.EndDate,
                HandleBy = !string.IsNullOrEmpty(Convert.ToString(row.HandleBy))
                    ? JsonConvert.DeserializeObject<HandleBy>(Convert.ToString(row.HandleBy)!)
                    : new HandleBy(),
                TimeLineType = (TimeLineTypeEnum)row.TimeLineType,
                ParentID = row.ParentID,
                Status = (EventTimeLineStatusEnum)row.Status
            }).ToList();

            const string countQuery = @"
                                    SELECT COUNT(*)
                                    FROM event_timeline
                                    WHERE event_id = @eventID
                                    AND (@status IS NULL OR status = @status)";

            int totalRecords = await connection.ExecuteScalarAsync<int>(countQuery, new { eventID, status = statusEnum });

            return new PagedResponse<List<EventTimeLineDTO>>(
                timeLineList,
                request.PageNumber,
                request.PageSize,
                (int)Math.Ceiling((double)totalRecords / request.PageSize),
                totalRecords
            );
        }

    }
}

