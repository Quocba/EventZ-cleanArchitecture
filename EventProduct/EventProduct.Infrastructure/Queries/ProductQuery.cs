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
    public class ProductQuery(ISqlConnectionFactory _connectionFactory) : IProductQuery
    {
        public async Task<PagedResponse<List<GetAllProductDTO>>> GetAllProduct(PagedRequestWithSearch request, string? name)
        {
            using var connection = _connectionFactory.GetOpenConnection();
            const string sqlQuery = @"
                                    SELECT
                                    p.id AS ProductID,
                                    p.name AS Name, 
                                    p.description AS Description, 
                                    p.sell_price AS SellPrice, 
                                    p.sale_price AS SalePrice, 
                                    p.stock AS Stock, 
                                    p.thumbnai_url AS ThumbnaiURL, 
                                    p.image_url AS ImageURL, 
                                    p.category_id AS CategoryID,
                                    c.name AS CategoryName, 
                                    c.description AS CategoryDescription
                                    FROM event_product p
                                    LEFT JOIN event_category c ON p.category_id = c.id
                                    WHERE (@Name IS NULL OR LTRIM(RTRIM(@Name)) = '' OR p.name LIKE '%' + @Name + '%')
                                    ORDER BY p.id
                                    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

                                   SELECT COUNT(*) 
                                   FROM event_product p
                                   WHERE (@Name IS NULL OR LTRIM(RTRIM(@Name)) = '' OR p.name LIKE '%' + @Name + '%');
                                ";

            var paramenters = new
            {
                Offset = (request.PageNumber - 1) * request.PageSize,
                Name = name,
                request.PageSize
            };

            using var mutil = await connection.QueryMultipleAsync(sqlQuery, paramenters);
            var listProduct = (await mutil.ReadAsync<GetAllProductDTO>()).ToList();
            var totalRecords = await mutil.ReadSingleAsync<int>();

            return new PagedResponse<List<GetAllProductDTO>>(
                          listProduct,
                          request.PageNumber,
                          request.PageSize,
                          (int)Math.Ceiling((double)totalRecords / request.PageSize),
                          totalRecords
             );
        }

    public async Task<PagedResponse<List<GetAllProductDTO>>> GetProductByCategory(PagedRequestWithSearch request, Guid categoryId, string? name)
        {
            using var connection = _connectionFactory.GetOpenConnection();

            const string sqlQuery = @"
                                    SELECT
                                    p.category_id AS CategoryID,
                                    c.name AS CategoryName, 
                                    c.description AS CategoryDescription,
                                    p.id AS ProductID,
                                    p.name AS Name, 
                                    p.description AS Description, 
                                    p.sell_price AS SellPrice, 
                                    p.sale_price AS SalePrice, 
                                    p.stock AS Stock, 
                                    p.thumbnai_url AS ThumbnaiURL, 
                                    p.image_url AS ImageURL
                                    FROM event_product p
                                    LEFT JOIN event_category c ON p.category_id = c.id 
                                    WHERE 
                                    p.category_id = @CategoryID  
                                    AND (@Name IS NULL OR LTRIM(RTRIM(@Name)) = '' OR p.name LIKE '%' + @Name + '%')
                                    ORDER BY p.id
                                    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

                                    SELECT COUNT(*) 
                                    FROM event_product p
                                    WHERE 
                                    p.category_id = @CategoryID
                                    AND (@Name IS NULL OR LTRIM(RTRIM(@Name)) = '' OR p.name LIKE '%' + @Name + '%');";
            var parameters = new
            {
                CategoryID = categoryId,
                Offset = (request.PageNumber - 1) * (request.PageSize - 1),
                Name = name,
                request.PageSize
            };

            using var mutil = await connection.QueryMultipleAsync(sqlQuery, parameters);
            var listProduct = (await mutil.ReadAsync<GetAllProductDTO>()).ToList();
            var totalRecords = await mutil.ReadSingleAsync<int>();

            return new PagedResponse<List<GetAllProductDTO>>(
                          listProduct,
                          request.PageNumber,
                          request.PageSize,
                          (int)Math.Ceiling((double)totalRecords / request.PageSize),
                          totalRecords
             );
        }
    }
}
