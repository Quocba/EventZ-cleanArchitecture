using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Event.Application.DTO;
using Event.Application.Interface;
using Event.Application.Wrappers;
using Event.Infrastructure.Context;

namespace Event.Infrastructure.Queries
{
    public class EventBookingQuery(ISqlConnectionFactory _connectionFactory) : IEventBookingQuery
    {
        public async Task<PagedResponseWithTotal<List<EventBookingDTO>>> GetBookingWithStatus(PagedRequest request, Guid eventID, int status)
        {
            using var connection = _connectionFactory.GetOpenConnection();
                        const string sqlQuery = @"
                                    SELECT 
                                        eb.id AS EventBookingID, 
                                        eb.num_seats AS NumSeat, 
                                        eb.additional_info AS AdditionalInfo, 
                                        eb.is_active AS IsActive, 
                                        eb.status AS Status, 
                                        eb.created_at AS CreatedAt, 
                                        eb.updated_at AS UpdatedAt, 
                                        eb.event_seat_id AS EventSeatID, 
                                        eb.user_id AS ID, 
                                        eb.payment_id AS PaymentID,
                                    COUNT(*) OVER() AS TotalNumberOfAttendess
                                    FROM event_booking eb
                                    INNER JOIN event_seats es ON eb.event_seat_id = es.id
                                    INNER JOIN event_layout el ON es.event_layout_id = el.id
                                    WHERE el.event_id = @eventID
                                    AND eb.status = @status
                                    ORDER BY eb.id
                                    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

                                    SELECT COUNT(*) 
                                    FROM event_booking eb
                                    INNER JOIN event_seats es ON eb.event_seat_id = es.id
                                    INNER JOIN event_layout el ON es.event_layout_id = el.id
                                    WHERE el.event_id = @eventID
                                    AND eb.status = @status;";

            var parameters = new
            {
                EventID = eventID,
                Status = status,
                request.PageSize,
                Offset = (request.PageNumber - 1) * request.PageSize,
            };

            using var mutil = await connection.QueryMultipleAsync(sqlQuery, parameters);
            var bookings = (await mutil.ReadAsync<EventBookingDTO>()).AsList();
            var totalRecords = await mutil.ReadFirstAsync<int>();

            return new PagedResponseWithTotal<List<EventBookingDTO>>(
                bookings,
                request.PageNumber,
                request.PageSize,
                (int)Math.Ceiling((double)totalRecords / request.PageSize),
                totalRecords,
                totalRecords

            );
        }

        public async Task<PagedResponseWithTotal<List<EventBookingDTO>>> GetTotalNumberAddtendess(PagedRequest request, Guid eventID)
        {
            using var connection = _connectionFactory.GetOpenConnection();
            const string sqlQuery = @"
                                    SELECT 
                                        eb.id AS EventBookingID, 
                                        eb.num_seats AS NumSeat, 
                                        eb.additional_info AS AdditionalInfo, 
                                        eb.is_active AS IsActive, 
                                        eb.status AS Status, 
                                        eb.created_at AS CreatedAt, 
                                        eb.updated_at AS UpdatedAt, 
                                        eb.event_seat_id AS EventSeatID, 
                                        eb.user_id AS ID, 
                                        eb.payment_id AS PaymentID,
                                    COUNT(*) OVER() AS TotalNumberOfAttendess
                                    FROM event_booking eb
                                    INNER JOIN event_seats es ON eb.event_seat_id = es.id
                                    INNER JOIN event_layout el ON es.event_layout_id = el.id
                                    WHERE el.event_id = @EventID
                                    ORDER BY eb.id
                                    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

                                    SELECT COUNT(*) 
                                    FROM event_booking eb
                                    INNER JOIN event_seats es ON eb.event_seat_id = es.id
                                    INNER JOIN event_layout el ON es.event_layout_id = el.id
                                    WHERE el.event_id = @EventID;";

            var parameters = new
            {
                EventID = eventID,
                request.PageSize,
                Offset = (request.PageNumber - 1) * request.PageSize,
            };

            using var mutil = await connection.QueryMultipleAsync(sqlQuery, parameters);
            var bookings = (await mutil.ReadAsync<EventBookingDTO>()).AsList();
            var totalRecords = await mutil.ReadFirstAsync<int>();

            return new PagedResponseWithTotal<List<EventBookingDTO>>(
                bookings,
                request.PageNumber,
                request.PageSize,
                (int)Math.Ceiling((double)totalRecords / request.PageSize),
                totalRecords,
                totalRecords
               
            );

        }
    }
}
