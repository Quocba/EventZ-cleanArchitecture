using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using EventProduct.Application.DTO;
using EventProduct.Application.Interface;
using EventProduct.Application.Wrappers;
using EventProduct.Infrastructure.Context;

namespace EventProduct.Infrastructure.Queries
{
    public class CategoryQuery(ISqlConnectionFactory _connectionFactory) : ICategoryQuery
    {
        public async Task<PagedResponse<List<GetAllCategoryDTO>>> GetAllCategory(PagedRequest request)
        {
            using var connection = _connectionFactory.GetOpenConnection();
            const string sqlQuery = @"SELECT id as ID,
                                    name AS Name, description AS Description
                                    FROM event_category
                                    ORDER BY id
                                    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY

                                    SELECT COUNT(id) FROM event_category";
            var parameter = new
            {
                Offset = (request.PageNumber - 1) * request.PageSize,
                request.PageSize,
            };

            using var multi = await connection.QueryMultipleAsync(sqlQuery, parameter);
            var getListCategory = (await multi.ReadAsync<GetAllCategoryDTO>()).ToList();
            var totalRecords = await multi.ReadSingleAsync<int>();
            
            return new PagedResponse<List<GetAllCategoryDTO>>(
                    getListCategory,
                    request.PageNumber,
                    request.PageSize,
                    (int)Math.Ceiling((double)totalRecords / request.PageSize),
                    totalRecords
            );
        }

        public async Task<PagedResponse<List<GetAllCategoryDTO>>> GetCategoryByEvent(PagedRequest request, Guid EventID)
        {
            using var connection = _connectionFactory.GetOpenConnection();
            const string sqlQuery = @"SELECT id AS ID,
                                      name AS Name, description AS Description
                                      FROM event_category
                                      Where event_id = @EventID
                                      ORDER BY id
                                      OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
                                        
                                      SELECT COUNT(id) FROM event_category
                                      WHERE event_id = @EventID;";
            var parameter = new
            {
                Offset = (request.PageNumber - 1) * request.PageSize,
                request.PageSize,
                EventID = EventID
            };

            using var muti = await connection.QueryMultipleAsync(sqlQuery, parameter);
            var getListCategory = (await muti.ReadAsync<GetAllCategoryDTO>()).ToList();
            int totalRecords = await muti.ReadSingleAsync<int>();

            return new PagedResponse<List<GetAllCategoryDTO>>(
                    getListCategory,
                    request.PageNumber,
                    request.PageSize,
                    (int)Math.Ceiling((double)totalRecords / request.PageSize),
                    totalRecords
            );

        }
    }
}
