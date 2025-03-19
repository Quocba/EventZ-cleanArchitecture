using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Event.Application.DTO;
using Event.Application.Interface;
using Event.Infrastructure.Context;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Utilities;

namespace Event.Infrastructure.Queries
{
    public class EventRegistrationLinkQuery(ISqlConnectionFactory _connectionFactory, IConfiguration _configuration) : IEventRegistrationLinkQuery
    {
        public async Task<List<GetEventRegistrationLinkDTO>> GetEventRegistrationLinks(Guid eventID)
        {
           using var connection =  _connectionFactory.GetOpenConnection();
            const string sqlQuery = @"Select code AS Code, start_date AS StartDate, end_date AS EndDate 
                                      From event_registration_link
                                      Where event_id = @eventID";

            var result = await connection.QueryAsync<GetEventRegistrationLinkDTO>(sqlQuery, new { eventID });
            foreach (var link in result)
            {
                link.Code = _configuration["Hosting:Host"]+"/" + link.Code; 
            }
            return result.ToList();
        }
    }
}
