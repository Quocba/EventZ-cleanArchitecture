using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Event.Application.Interface;
using Event.Application.Wrappers;
using Event.Domain.Entities;
using Event.Infrastructure.Context;

namespace Event.Infrastructure.Queries
{
    public class EventTypeQuery(ISqlConnectionFactory _connectionFactory) : IEventTypeQuery
    {
        public async Task<PagedResponse<List<EventType>>> GetAllEventType(PagedRequest request)
        { 
            using var connection = _connectionFactory.GetOpenConnection();

            const string sqlQuery = @"
        WITH event_ids AS (
            SELECT id
            FROM event_type
            ORDER BY id
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY
        )
        SELECT 
            et.id, 
            et.name, 
            et.description
        FROM event_ids ei_ids
        INNER JOIN event_type et ON ei_ids.id = et.id;";

            var offset = (request.PageNumber - 1) * request.PageSize;

            var eventTypes = (await connection.QueryAsync<EventType>(
                sqlQuery,
                new { Offset = offset, PageSize = request.PageSize }
            )).ToList();

            const string sqlCount = "SELECT COUNT(id) FROM event_type";
            var totalRecords = await connection.ExecuteScalarAsync<int>(sqlCount);

            return new PagedResponse<List<EventType>>(
                eventTypes,
                request.PageNumber,
                request.PageSize,
                totalRecords,
                (int)Math.Ceiling((double)totalRecords / request.PageSize)
            );
        }

        public async Task<EventType> GetByID(Guid eventTypeID)
        {
            using var connection = _connectionFactory.GetOpenConnection();
            const string sqlQuery = $"SELECT * FROM event_type Where id = @eventTypeID ";
            var result = await connection.QueryFirstOrDefaultAsync<EventType>(sqlQuery, new { eventTypeID });

            return result;
        }
    }
}
