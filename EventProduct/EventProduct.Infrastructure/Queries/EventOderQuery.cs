using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using EventProduct.Application.DTO;
using EventProduct.Application.Wrappers;
using EventProduct.Domain.Entities;
using EventProduct.Infrastructure.Context;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RabbitMQ.Contract.DTO;
using RabbitMQ.Contract.DTO.GetEvent;

namespace EventProduct.Infrastructure.Queries
{
    public class EventOderQuery(ISqlConnectionFactory _connectionFactory) : IEventOrderQuery
    {
        public async Task<PagedResponse<List<EventOrderDTO>>> GetEventOrderByPayment(PagedRequest request, Guid paymentID)
        {
            var connection = _connectionFactory.GetOpenConnection();
            const string sqlQuery = @"
                                      SELECT 
                                      id AS EventOderID,  
                                      total_price AS TotalPrice,
                                      created_at AS CreatedAt, update_at AS UpdateAt,
                                      fullname AS FullName, email AS Email,
                                      Phone AS Phone, address AS Address,
                                      event_id AS EventID, user_id AS UserID,
                                      payment_history_id AS PaymentHistoryID
                                      FROM event_order
                                      WHERE payment_history_id = @paymentID   
                                      Order BY id
                                      OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY

                                      SELECT COUNT(id) 
                                      FROM event_order
                                      WHERE payment_history_id = @paymentID
                                      ";

            var parameters = new
            {
                paymentID = paymentID,
                request.PageSize,
                Offset = (request.PageNumber - 1) * request.PageSize,
            };

            using var mutil = await connection.QueryMultipleAsync(sqlQuery, parameters);
            var listEventOrder = (await mutil.ReadAsync<EventOrderDTO>()).ToList();
            var totalRecords = await mutil.ReadSingleAsync<int>();

            return new PagedResponse<List<EventOrderDTO>>(
                    listEventOrder,
                    request.PageNumber,
                    request.PageSize,
                    (int)Math.Ceiling((double)totalRecords / request.PageSize),
                    totalRecords
                );
        }


        public async Task<PagedResponse<List<EventOrderDTO>>> GetEventOrderByUser(PagedRequest request, Guid userID)
        {
            var connection = _connectionFactory.GetOpenConnection();
            const string sqlQuery = @"
                                      SELECT 
                                      id AS EventOderID,  
                                      total_price AS TotalPrice,
                                      created_at AS CreatedAt, update_at AS UpdateAt,
                                      fullname AS FullName, email AS Email,
                                      phone AS Phone, address AS Address,
                                      event_id AS EventID, user_id AS UserID,
                                      payment_history_id AS PaymentHistoryID
                                      FROM event_order
                                      WHERE user_id = @userID   
                                      Order BY id
                                      OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY

                                      SELECT COUNT(id)
                                      FROM event_order
                                      WHERE user_id = @userID   
                                      ";

            var parameters = new
            {
                userID = userID,
                request.PageSize,
                Offset = (request.PageNumber - 1) * request.PageSize,
            };

            using var mutil = await connection.QueryMultipleAsync(sqlQuery, parameters);
            var listEventOrder = (await mutil.ReadAsync<EventOrderDTO>()).ToList();
            var totalRecords = await mutil.ReadSingleAsync<int>();

            return new PagedResponse<List<EventOrderDTO>>(
                    listEventOrder,
                    request.PageNumber,
                    request.PageSize,
                    (int)Math.Ceiling((double)totalRecords / request.PageSize),
                    totalRecords
                );
        }


        public async Task<PagedResponse<List<EventOrderDTO>>> GetEventOrderByEvent(PagedRequest request, Guid eventID)
        {
            var connection = _connectionFactory.GetOpenConnection();
            const string sqlQuery = @"
                                      SELECT 
                                      id AS EventOderID,  
                                      total_price AS TotalPrice,
                                      created_at AS CreatedAt, update_at AS UpdateAt,
                                      fullname AS FullName, email AS Email,
                                      phone AS Phone, address AS Address,
                                      event_id AS EventID, user_id AS UserID,
                                      payment_history_id AS PaymentHistoryID
                                      FROM event_order
                                      WHERE event_id = @eventID   
                                      Order BY id
                                      OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY

                                      SELECT COUNT(id)
                                      FROM event_order
                                      WHERE event_id = @eventID
                                      ";

            var parameters = new
            {
                eventID = eventID,
                request.PageSize,
                Offset = (request.PageNumber - 1) * request.PageSize,
            };

            using var mutil = await connection.QueryMultipleAsync(sqlQuery, parameters);
            var listEventOrder = (await mutil.ReadAsync<EventOrderDTO>()).ToList();
            var totalRecords = await mutil.ReadSingleAsync<int>();

            return new PagedResponse<List<EventOrderDTO>>(
                    listEventOrder,
                    request.PageNumber,
                    request.PageSize,
                    (int)Math.Ceiling((double)totalRecords / request.PageSize),
                    totalRecords
                );
        }

        public async Task<int> SumaryEventOrder(Guid eventID)
        {

            using var connection = _connectionFactory.GetOpenConnection();
            const string sqlQuery = @"SELECT COUNT(id)
                                      FROM event_order
                                      Where event_id = @eventID";
            var parameter = new
            {
                EventID = eventID,
            };
            var totalOrders = await connection.ExecuteScalarAsync<int>(sqlQuery, parameter);
            return totalOrders;

        }

        public async Task<decimal> TotalRevenueProduct(Guid eventID)
        {
            using var connection = _connectionFactory.GetOpenConnection();
            const string sqlQuery = @"
                                    Select SUM(ovp.total_price)
                                    From event_order ovp
                                    Where event_id = @eventID
                                    ";
            var parameter = new
            {
                EventID = eventID 
            };

            using var mutil = await connection.QueryMultipleAsync(sqlQuery, parameter);
            var totalRevenue = await mutil.ReadSingleAsync<decimal>();
            return totalRevenue;
        }

    }
}
