using Dapper;
using Event.Application.DTO;
using Event.Application.Interface;
using Event.Application.Wrappers;
using Event.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Infrastructure.Queries
{
    public class LayoutQuery(ISqlConnectionFactory connectionFactory) : ILayoutQuery
    {
        public async Task<PagedResponse<List<LayoutResponse>>> GetAllLayout(PagedRequestWithSearch request)
        {
            using var connection = connectionFactory.GetOpenConnection();

            const string sql = @"
                  SELECT
	                l.id,
	                l.layout_name AS LayoutName,
	                l.layout_floor_number AS LayoutFloorNumber,
	                l.layout_type AS LayoutType,
	                l.rows,
	                l.cols,
	                l.created_by AS CreateBy
                  FROM layout l
                WHERE l.IsDeleted = 0
                    AND (@Search IS NULL OR LOWER(l.layout_name) LIKE '%' + LOWER(@Search) + '%')
                ORDER BY l.created_at DESC
                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
            ";

            var offset = (request.PageNumber - 1) * request.PageSize;

            var layouts = await connection.QueryAsync<LayoutResponse>(sql, new
            {
                Offset = offset,
                request.PageSize,
                request.Search,
            });

            const string sqlCount = @"
                SELECT COUNT(*) 
                FROM layout l
                WHERE l.IsDeleted = 0
                    AND (@Search IS NULL OR 
                        LOWER(l.layout_name) LIKE '%' + LOWER(@Search) + '%')
            ";

            var totalRecords = await connection.ExecuteScalarAsync<int>(sqlCount, new
            {
                request.Search,
            });

            var response = new PagedResponse<List<LayoutResponse>>(
                layouts.AsList(),
                request.PageNumber,
                request.PageSize,
                (int)Math.Ceiling((double)totalRecords / request.PageSize),
                totalRecords
            );

            return response;
        }

        public Task<LayoutResponse> GetLayout(Guid layoutId)
        {
            throw new NotImplementedException();
        }
    }
}
