using Dapper;
using Identity.Application.DTO;
using Identity.Application.Exceptions;
using Identity.Application.Interfaces;
using Identity.Application.Wrappers;
using Identity.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Infrastructure.Queries
{
    public class UserQuery(ISqlConnectionFactory connectionFactory) : IUserQuery
    {
        public async Task<PagedResponse<List<UserListResponse>>> GetUsers(PagedRequestWithSearch request)
        {
            using var connection = connectionFactory.GetOpenConnection();

            const string sql = @"
                SELECT 
                    u.id,
                    u.first_name AS FirstName,
                    u.last_name AS LastName,
                    u.email
                FROM users u
                WHERE u.is_active = 1
                    AND (@Search IS NULL OR 
                        LOWER(u.first_name) LIKE '%' + LOWER(@Search) + '%' OR 
                        LOWER(u.last_name) LIKE '%' + LOWER(@Search) + '%' OR 
                        LOWER(u.email) LIKE '%' + LOWER(@Search) + '%')
                ORDER BY u.create_at DESC
                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

                SELECT ur.user_id AS UserId, r.name AS Role
                FROM user_roles ur
                INNER JOIN roles r ON ur.role_id = r.id
                WHERE ur.user_id IN (
                    SELECT u.id
                    FROM users u
                    WHERE u.is_active = 1
                        AND (@Search IS NULL OR 
                            LOWER(u.first_name) LIKE '%' + LOWER(@Search) + '%' OR 
                            LOWER(u.last_name) LIKE '%' + LOWER(@Search) + '%' OR 
                            LOWER(u.email) LIKE '%' + LOWER(@Search) + '%')
                    ORDER BY u.create_at DESC
                    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY
                );
            ";

            var offset = (request.PageNumber - 1) * request.PageSize;

            using var multi = await connection.QueryMultipleAsync(sql, new
            {
                Offset = offset,
                request.PageSize,
                request.Search
            });

            var users = (await multi.ReadAsync<UserListResponse>()).ToList();
            var userRoles = (await multi.ReadAsync<(Guid UserId, string Role)>()).ToList();

            var rolesDict = userRoles
                .GroupBy(ur => ur.UserId)
                .ToDictionary(g => g.Key, g => g.Select(r => r.Role).ToList());

            foreach (var user in users)
            {
                user.Roles = rolesDict.TryGetValue(user.Id, out var roles) ? roles : new List<string>();
            }

            const string sqlCount = @"
                SELECT COUNT(*) 
                FROM users 
                WHERE is_active = 1
                    AND (@Search IS NULL OR 
                        LOWER(first_name) LIKE '%' + LOWER(@Search) + '%' OR 
                        LOWER(last_name) LIKE '%' + LOWER(@Search) + '%' OR 
                        LOWER(email) LIKE '%' + LOWER(@Search) + '%')";

            var totalRecords = await connection.ExecuteScalarAsync<int>(sqlCount, new
            {
                request.Search
            });

            var response = new PagedResponse<List<UserListResponse>>(
                users,
                request.PageNumber,
                request.PageSize,
                (int)Math.Ceiling((double)totalRecords / request.PageSize),
                totalRecords
            );

            return response;
        }

        public async Task<UserResponse> GetUser(Guid id)
        {
            using var connection = connectionFactory.GetOpenConnection();

            const string sqlUser = @"
                SELECT 
                    u.id,
                    u.first_name AS FirstName,
                    u.last_name AS LastName,
                    u.username,
                    u.email,
                    u.is_email_confirmed AS IsEmailConfirmed,
                    u.phone,
                    u.is_phone_confirmed AS IsPhoneConfirmed,
                    u.is_active,
                    u.create_at AS CreateAt,
                    u.gender,
                    u.last_modified_at AS LastModifiedAt
                FROM users u
                WHERE u.id = @Id;";

            const string sqlRoles = @"
                SELECT r.name 
                FROM user_roles ur
                INNER JOIN roles r ON ur.role_id = r.id
                WHERE ur.user_id = @Id;";

            var user = await connection.QueryFirstOrDefaultAsync<UserResponse>(sqlUser, new { Id = id });

            if (user == null) return null;

            var roles = await connection.QueryAsync<string>(sqlRoles, new { Id = id });
            user.Roles = [.. roles];

            return user;
        }

        public async Task<PagedResponse<List<EventUserListResponse>>> GetEventUsers(PagedRequestEventUser request)
        {
            using var connection = connectionFactory.GetOpenConnection();

            const string sql = @"
                SELECT
                    u.id,
                    u.first_name AS FirstName,
                    u.last_name AS LastName,
                    u.email,
                    er.name AS EventRoleName
                FROM users u
                INNER JOIN user_event uv ON u.id = uv.user_id
                INNER JOIN event_roles er ON er.id = uv.event_role_id
                WHERE u.is_active = 1
                    AND uv.event_id = @EventId
                    AND (@Search IS NULL OR 
                        LOWER(u.first_name) LIKE '%' + LOWER(@Search) + '%' OR 
                        LOWER(u.last_name) LIKE '%' + LOWER(@Search) + '%' OR 
                        LOWER(u.email) LIKE '%' + LOWER(@Search) + '%')
                    AND (@EventRoleIds IS NULL OR uv.event_role_id IN (SELECT value FROM STRING_SPLIT(@EventRoleIds, ',')))
                ORDER BY u.create_at DESC
                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
            ";

            var offset = (request.PageNumber - 1) * request.PageSize;

            var eventRoleIds = request.EventRoleIds != null && request.EventRoleIds.Any()
                ? string.Join(",", request.EventRoleIds.Select(id => id.ToString()))
                : null;

            var users = await connection.QueryAsync<EventUserListResponse>(sql, new
            {
                Offset = offset,
                request.PageSize,
                request.Search,
                request.EventId,
                EventRoleIds = eventRoleIds
            });

            const string sqlCount = @"
                SELECT COUNT(*) 
                FROM users u
                INNER JOIN user_event uv ON u.id = uv.user_id
                WHERE u.is_active = 1
                    AND uv.event_id = @EventId
                    AND (@Search IS NULL OR 
                        LOWER(u.first_name) LIKE '%' + LOWER(@Search) + '%' OR 
                        LOWER(u.last_name) LIKE '%' + LOWER(@Search) + '%' OR 
                        LOWER(u.email) LIKE '%' + LOWER(@Search) + '%')
                    AND (@EventRoleIds IS NULL OR uv.event_role_id IN (SELECT value FROM STRING_SPLIT(@EventRoleIds, ',')))
            ";

            var totalRecords = await connection.ExecuteScalarAsync<int>(sqlCount, new
            {
                request.Search,
                request.EventId,
                EventRoleIds = eventRoleIds
            });

            var response = new PagedResponse<List<EventUserListResponse>>(
                users.AsList(),
                request.PageNumber,
                request.PageSize,
                (int)Math.Ceiling((double)totalRecords / request.PageSize),
                totalRecords
            );

            return response;
        }

        public async Task<EventUserResponse> GetEventUser(Guid id)
        {
            using var connection = connectionFactory.GetOpenConnection();

            const string sqlUser = @"
                SELECT 
                    u.id,
                    u.first_name AS FirstName,
                    u.last_name AS LastName,
                    u.username,
                    u.email,
                    u.is_email_confirmed AS IsEmailConfirmed,
                    u.phone,
                    u.is_phone_confirmed AS IsPhoneConfirmed,
                    u.is_active,
                    u.create_at AS CreateAt,
                    u.gender,
                    u.last_modified_at AS LastModifiedAt,
                    er.name AS EventRoleName
                FROM users u
                INNER JOIN user_event uv ON u.id = uv.user_id
                INNER JOIN event_roles er ON er.id = uv.event_role_id
                WHERE u.id = @Id;";

            var user = await connection.QueryFirstOrDefaultAsync<EventUserResponse>(sqlUser, new { Id = id });

            if (user == null) return null;

            return user;
        }
    }
}
