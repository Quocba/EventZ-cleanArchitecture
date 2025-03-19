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
    public class EventOrderProductQuery(ISqlConnectionFactory _connectionFactory) : IEventOrderProductQuery
    {
        public async Task<PagedResponse<List<GetEventOrderProductDTO>>> GetEventOrderProductByEventOrder(PagedRequest request, Guid eventOrderID)
        {
            using var connection = _connectionFactory.GetOpenConnection();
            const string sqlQuery = @" 
                                    SELECT 
                                    eop.id AS Id,
                                    eop.price AS Price,
                                    eop.created_at AS CreatedAt,
                                    eop.update_at AS UpdatedAt,
                                    eop.quantity AS Quantity,
                                    ep.id AS ProductID,
                                    ep.name AS ProductName,
                                    ep.description AS ProductDescription,
                                    ep.sell_price AS SellPrice,
                                    ep.sale_price AS SalePrice,
                                    ep.stock AS Stock,
                                    ep.thumbnai_url AS ThumbnailURL,
                                    ep.image_url AS ImageURL,
                                    ec.id AS CategoryID,
                                    ec.name AS CategoryName
                                    FROM event_order_product eop
                                    INNER JOIN event_product ep ON eop.product_id = ep.id
                                    INNER JOIN event_category ec ON ep.category_id = ec.id
                                    WHERE eop.event_order_id = @eventOrderID
                                    ORDER BY eop.id
                                    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY
                                    
                                    SELECT COUNT(id) 
                                    FROM event_order_product 
                                    WHERE event_order_id = @eventOrderID;";
            var parameters = new
            {
                EventOrderID = eventOrderID,
                request.PageSize,
                Offset = (request.PageNumber - 1) * request.PageSize,
            };

            using var mutil = await connection.QueryMultipleAsync(sqlQuery, parameters);
            var listProduct = (await mutil.ReadAsync<GetEventOrderProductDTO>()).ToList();
            var totalRecords = await mutil.ReadSingleAsync<int>();

            return new PagedResponse<List<GetEventOrderProductDTO>>(
                listProduct,
                request.PageNumber,
                request.PageSize,
                (int)Math.Ceiling((double)totalRecords / request.PageSize),
                totalRecords
                );
            throw new NotImplementedException();
        }

        public async Task<int> StatisticalProductSaled(Guid eventID)
        {
            using var connection = _connectionFactory.GetOpenConnection();
            const string query = @"SELECT 
                            COALESCE(SUM(eop.quantity), 0) AS total_products_sold
                            FROM event_order_product eop
                            JOIN event_order e ON eop.event_order_id = e.id
                            WHERE e.event_id = @eventID;";
            var paramenter = new
            {
                EventID = eventID,
            };

            int totalProduct = await connection.QueryFirstOrDefaultAsync<int>(query, paramenter);

            return totalProduct;
        }
    }
}
