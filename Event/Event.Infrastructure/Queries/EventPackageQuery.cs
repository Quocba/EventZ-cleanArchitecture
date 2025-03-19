using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Event.Application.Interface;
using Event.Application.Wrappers;
using Event.Domain.Entities;
using Event.Domain.Entities.Json;
using Event.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Event.Infrastructure.Queries
{
    public class EventPackageQuery(ISqlConnectionFactory _connectionFactory) : IEventPackageQuery
    {
        public async Task<PagedResponse<List<EventPackages>>> GetAllEventPackage(PagedRequest request)
        {
            using var connection = _connectionFactory.GetOpenConnection();

            const string sqlQuery = @"
                                    WITH event_ids AS (
                                        SELECT id
                                        FROM event_packages
                                        ORDER BY id
                                        OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY
                                    )
                                    SELECT 
                                        ep.id, 
                                        ep.name, 
                                        ep.sell_price AS SellPrice, 
                                        ep.sale_price AS SalePrice, 
                                        ep.benifit
                                    FROM event_ids ei
                                    INNER JOIN event_packages ep ON ei.id = ep.id;";

            var offset = (request.PageNumber - 1) * request.PageSize;

            using var multi = await connection.QueryMultipleAsync(sqlQuery, new { Offset = offset, PageSize = request.PageSize });

            var eventPackages = (await multi.ReadAsync<dynamic>()).Select(row => new EventPackages
            {
                Id = row.id,
                Name = row.name,
                SellPrice = row.SellPrice,
                SalePrice = row.SalePrice,
                Benefit = !string.IsNullOrEmpty(Convert.ToString(row.benifit))
                    ? JsonConvert.DeserializeObject<Benefit>(Convert.ToString(row.benifit)!)
                    : new Benefit()
            }).ToList();

            const string sqlCount = "SELECT COUNT(id) FROM event_packages";
            var totalRecords = await connection.ExecuteScalarAsync<int>(sqlCount);

            return new PagedResponse<List<EventPackages>>(
                eventPackages,
                request.PageNumber,
                request.PageSize,
                (int)Math.Ceiling((double)totalRecords / request.PageSize),
                totalRecords
            );

        }


    }
}
