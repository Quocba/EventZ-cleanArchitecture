using Dapper;
using Payment.Application.DTO;
using Payment.Application.Interfaces;
using Payment.Application.Wrappers;
using Payment.Domain.Entities;
using Payment.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Infrastructure.Queries
{
    public class PaymentEventQuery(ISqlConnectionFactory connectionFactory) : IPaymentEventQuery
    {
        public async Task<PagedResponse<List<PaymentEventListResponse>>> GetListPaymentEvent(PagedRequestPaymentEvent request)
        {
            using var connection = connectionFactory.GetOpenConnection();

            const string sql = @"
                    SELECT
                     p.id,
                     p.user_id AS UserId,
                     p.event_id AS EventId,
                     p.content,
                     p.amount,
                     p.status,
                     p.type,
                     p.created_at AS CreatedAt,
                     p.updated_at AS UpdatedAt,
                     p.expire_at AS ExpireAt
                    FROM payment_event p
                    WHERE p.status = 1 AND (@EventId IS NULL OR p.event_id = @EventId) 
                    ORDER BY p.created_at DESC
                    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
                ";

            var offset = (request.PageNumber - 1) * request.PageSize;

            var eventId = request.EventId == Guid.Empty ? (Guid?)null : request.EventId;

            var paymentEvents = await connection.QueryAsync<PaymentEventListResponse>(sql, new
            {
                Offset = offset,
                request.PageSize,
                EventId = eventId,
            });

            const string sqlCount = @"
                    SELECT Count(id)
                    FROM payment_event p
                    WHERE p.status = 1 AND (@EventId IS NULL OR p.event_id = @EventId) 
                ";

            var totalRecords = await connection.ExecuteScalarAsync<int>(sqlCount, new
            {
                EventId = eventId,
            });

            var response = new PagedResponse<List<PaymentEventListResponse>>(
                paymentEvents.AsList(),
                request.PageNumber,
                request.PageSize,
                (int)Math.Ceiling((double)totalRecords / request.PageSize),
                totalRecords
            );

            return response;
        }
        public async Task<PagedResponse<List<PaymentEventListResponse>>> GetListPaymentEventHistory(PagedRequestPaymentEventHistory request)
        {
            using var connection = connectionFactory.GetOpenConnection();

            const string sql = @"
                    SELECT
                     p.id,
                     p.user_id AS UserId,
                     p.event_id AS EventId,
                     p.content,
                     p.amount,
                     p.status,
                     p.type,
                     p.created_at AS CreatedAt,
                     p.updated_at AS UpdatedAt,
                     p.expire_at AS ExpireAt
                    FROM payment_event p
                    WHERE p.status = 1 AND (@UserId IS NULL OR p.user_id = @UserId) 
                    ORDER BY p.created_at DESC
                    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
                ";

            var offset = (request.PageNumber - 1) * request.PageSize;

            var userId = request.UserId == Guid.Empty ? (Guid?)null : request.UserId;

            var paymentEvents = await connection.QueryAsync<PaymentEventListResponse>(sql, new
            {
                Offset = offset,
                request.PageSize,
                UserId = userId,
            });

            const string sqlCount = @"
                    SELECT Count(id)
                    FROM payment_event p
                    WHERE p.status = 1 AND (@UserId IS NULL OR p.user_id = @UserId) 
                ";

            var totalRecords = await connection.ExecuteScalarAsync<int>(sqlCount, new
            {
                UserId = userId,
            });

            var response = new PagedResponse<List<PaymentEventListResponse>>(
                paymentEvents.AsList(),
                request.PageNumber,
                request.PageSize,
                (int)Math.Ceiling((double)totalRecords / request.PageSize),
                totalRecords
            );

            return response;
        }

        public async Task<PagedResponse<List<PaymentEventListResponse>>> GetListPaymentWithStatus(PagedRequest request,int status)
        {
            using var connection = connectionFactory.GetOpenConnection();

            const string sqlQuery = @"
                                    SELECT
                                     p.id,
                                     p.user_id AS UserId,
                                     p.event_id AS EventId,
                                     p.content,
                                     p.amount,
                                     p.status,
                                     p.type,
                                     p.created_at AS CreatedAt,
                                     p.updated_at AS UpdatedAt,
                                     p.expire_at AS ExpireAt
                                    FROM payment_event p
                                    WHERE p.status = @status 
                                    ORDER BY p.created_at DESC
                                    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
                                    
                                    SELECT COUNT(p.id) FROM payment_event p WHERE p.status = @status 
                                    ";
            var parameters = new
            {
                Status = status,
                request.PageSize,
                Offset = (request.PageNumber - 1) * request.PageSize
            };

            using var multi = await connection.QueryMultipleAsync(sqlQuery, parameters);
            var paymentList = (await multi.ReadAsync<PaymentEventListResponse>()).ToList();
            int totalRecords = await multi.ReadFirstAsync<int>();
                var response = new PagedResponse<List<PaymentEventListResponse>>(
                paymentList.AsList(),
                request.PageNumber,
                request.PageSize,
                (int)Math.Ceiling((double)totalRecords / request.PageSize),
                totalRecords
            );

            return response;

        }
    }
}
