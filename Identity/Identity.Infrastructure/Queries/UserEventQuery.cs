using Dapper;
using Identity.Application.Interfaces;
using Identity.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Queries
{
    public class UserEventQuery(ISqlConnectionFactory _connectFactory) : IUserEventQuery
    {
        public async Task<int> CountEventAttended(Guid? userId)
        {
            using var connection = _connectFactory.GetOpenConnection();

            const string countAttendedQuery = @"
                                    SELECT COUNT(*) 
                                    FROM user_event
                                    WHERE (@UserId IS NULL OR user_id = @UserId)"
            ;

            if (userId == Guid.Empty) userId = null;

            int totalAttended = await connection.ExecuteScalarAsync<int>(countAttendedQuery, new { UserId = userId });

            return totalAttended;
        }
    }
}
