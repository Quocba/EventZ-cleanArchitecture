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
    public class EventPackageRegistrationQuery(ISqlConnectionFactory _connectionFactory) : IEventPackageRegistrationQuery
    {
        public async Task<PagedResponse<List<GetEventPackageRegistrationDTO>>> GetAllEventPackageRegistration(PagedRequest request, int? status)
        {
            using var connection = _connectionFactory.GetOpenConnection();

            var offset = (request.PageNumber - 1) * request.PageSize;
            const string sqlQuery = @"
                                    WITH registrations AS (
                                        SELECT id
                                        FROM event_package_registrations
                                        WHERE (@Status IS NULL OR status = @Status)
                                        ORDER BY create_at DESC
                                        OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY
                                    )
                                    SELECT 
                                        epr.id,
                                        epr.name AS Name,
                                        epr.email AS Email,
                                        epr.phone AS Phone,
                                        epr.status AS Status,
                                        epr.create_at AS CreateAt,
                                        epr.update_at AS UpdateAt,
                                        epr.price AS Price,
                                        ep.name AS EventPackageName,
                                        ep.sell_price AS EventPackageSellPrice,
                                        ep.sale_price AS EventPackageSalePrice,
                                        ep.benifit AS EventPackageBenefit
                                    FROM registrations r
                                    INNER JOIN event_package_registrations epr ON r.id = epr.id
                                    LEFT JOIN event_packages ep ON epr.event_package_id = ep.id;
                                ";

            var result = await connection.QueryAsync<GetEventPackageRegistrationDTO>(
                sqlQuery,
                new { Status = status, Offset = offset, PageSize = request.PageSize }
            );

            const string sqlCount = @"
            SELECT COUNT(*) 
            FROM event_package_registrations
            WHERE (@Status IS NULL OR status = @Status)";

            int totalRecords = await connection.ExecuteScalarAsync<int>(sqlCount, new { Status = status });

            return new PagedResponse<List<GetEventPackageRegistrationDTO>>(
                result.ToList(),
                request.PageNumber,
                request.PageSize,
                (int)Math.Ceiling((double)totalRecords / request.PageSize),
                totalRecords
            );
        }


        public async Task<GetEventPackageRegistrationDTO> GetEventPackageRegistrationDetail(Guid eventPackageRegistrationID)
        {
            using var connection = _connectionFactory.GetOpenConnection();
            const string sql = @"
                        SELECT 
                            epr.id,
                            epr.name AS Name,
                            epr.email AS Email,
                            epr.phone AS Phone,
                            epr.status AS Status,
                            epr.create_at AS CreateAt,
                            epr.update_at AS UpdateAt,
                            epr.price AS Price,
                            ep.name AS EventPackageName,
                            ep.sell_price AS EventPackageSellPrice,
                            ep.sale_price AS EventPackageSalePrice,
                            ep.benifit AS EventPackageBenefit
                        FROM event_package_registrations epr
                        LEFT JOIN event_packages ep ON epr.event_package_id = ep.id
                        WHERE epr.id = @EventPackageRegistrationID;";

            var parameters = new { EventPackageRegistrationID = eventPackageRegistrationID };
            var result = await connection.QueryFirstOrDefaultAsync<GetEventPackageRegistrationDTO>(sql, parameters);
            return result;
        }

        public async Task<PagedResponse<List<GetEventPackageRegistrationDTO>>> GetEventPackageRegistrationToDay(PagedRequest request)
        {
            using var connection = _connectionFactory.GetOpenConnection();
            const string sql = @"
                        SELECT 
                            epr.id,
                            epr.name AS Name,
                            epr.email AS Email,
                            epr.phone AS Phone,
                            epr.status AS Status,
                            epr.create_at AS CreateAt,
                            epr.update_at AS UpdateAt,
                            epr.price AS Price,
                            ep.name AS EventPackageName,
                            ep.sell_price AS EventPackageSellPrice,
                            ep.sale_price AS EventPackageSalePrice,
                            ep.benifit AS EventPackageBenefit
                        FROM event_package_registrations epr
                        LEFT JOIN event_packages ep ON epr.event_package_id = ep.id
                        WHERE CAST(epr.create_at AS DATE) = CAST(GETDATE() AS DATE)
                        ORDER BY epr.create_at
                        OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY 

                        SELECT COUNT(epr.id)
                        FROM event_package_registrations epr
                        WHERE CAST(epr.create_at AS DATE) = CAST(GETDATE() AS DATE)
                        ";

            var paramenters = new
            {
                request.PageSize,
                Offset = (request.PageNumber - 1) * request.PageSize
            };

            using var mutil = await connection.QueryMultipleAsync(sql, paramenters);
            var listRegistrationToday = (await mutil.ReadAsync<GetEventPackageRegistrationDTO>()).ToList();
            int totalRecords = mutil.ReadSingleOrDefault<int>();
            try
            {
                return new PagedResponse<List<GetEventPackageRegistrationDTO>>(
                    listRegistrationToday,
                    request.PageNumber,
                    request.PageSize,
                    (int)Math.Ceiling((double)totalRecords / request.PageSize),
                    totalRecords
                );
            }
            catch (Exception ex)
            {
                throw new Exception($"Serialization error: {ex.Message}", ex);
            }
        }

        public async Task<PagedResponse<List<GetEventPackageRegistrationDTO>>> GetRegistrationByEventPackage(PagedRequest request, Guid eventPackageID)
        {
            using var connection = _connectionFactory.GetOpenConnection();

            const string sqlQuery = @"
                                    WITH registration_ids AS (
                                        SELECT id 
                                        FROM event_package_registrations 
                                        WHERE event_package_id = @eventPackageID
                                        ORDER BY create_at DESC
                                        OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY
                                    )
                                    SELECT 
                                        epr.id,
                                        epr.name AS Name,
                                        epr.email AS Email,
                                        epr.phone AS Phone,
                                        epr.status AS Status,
                                        epr.create_at AS CreateAt,
                                        epr.update_at AS UpdateAt,
                                        epr.price AS Price
                                    FROM registration_ids r
                                    INNER JOIN event_package_registrations epr ON r.id = epr.id;
                                ";
            var offset = (request.PageNumber - 1) * request.PageSize;

            var result = await connection.QueryAsync<GetEventPackageRegistrationDTO>(
                sqlQuery,
                new { eventPackageID, Offset = offset, PageSize = request.PageSize }
            );

            const string sqlCount = @"
                                    SELECT COUNT(*) 
                                    FROM event_package_registrations
                                    WHERE event_package_id = @eventPackageID";

            int totalRecords = await connection.ExecuteScalarAsync<int>(sqlCount, new { eventPackageID });

            return new PagedResponse<List<GetEventPackageRegistrationDTO>>(
                result.ToList(),
                request.PageNumber,
                request.PageSize,
                (int)Math.Ceiling((double)totalRecords / request.PageSize),
                totalRecords
            );
        }

    }
}

