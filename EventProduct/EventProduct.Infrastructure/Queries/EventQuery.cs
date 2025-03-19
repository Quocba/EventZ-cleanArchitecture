//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Net.Http.Json;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;
//using Dapper;
//using EventProduct.Application.DTO;
//using EventProduct.Application.Interface;
//using EventProduct.Application.Wrappers;
//using EventProduct.Domain.Entities;
//using EventProduct.Infrastructure.Context;
//using MassTransit;
//using Microsoft.Extensions.Configuration;

//namespace EventProduct.Infrastructure.Queries
//{
//    public class EventQuery(ISqlConnectionFactory connectionFactory, IConfiguration _configuration) : IEventQuery
//    {
//        public async Task<PagedResponse<List<GetAllEventDTO>>> GetAllEvent(PagedRequest request, string? title, bool? isDeleted)
//        {
//            using var connection = connectionFactory.GetOpenConnection();
//            const string sqlQuery =
//                                    @"Select title AS Title, description AS Description,
//                                      start_time AS StartTime, end_time AS EndTime,
//                                      province AS Province, address AS Address,
//                                      additional_info AS AdditionalInfo,
//                                      event_type_id AS EventTypeID,
//                                      is_deleted AS IsDeleted,
//                                      user_id AS UserID
//                                      From events
//                                      WHERE (@Title IS NULL OR title LIKE '%' + @Title + '%')
//                                      AND (@IsDeleted IS NULL OR is_deleted = @IsDeleted)
//                                      ORDER BY id
//                                      OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY
                    
//                                      SELECT COUNT(*) FROM events
//                                      WHERE (@Title IS NULL OR title LIKE '%' + @Title + '%')
//                                      AND (@IsDeleted IS NULL OR is_deleted = @IsDeleted);
//                                    ";
//            var parameters = new
//            {
//                Title = title,
//                IsDeleted = isDeleted,
//                Offset = (request.PageNumber - 1) * request.PageSize,
//                request.PageSize
//            };

//            using var multi = await connection.QueryMultipleAsync(sqlQuery, parameters);
//            var events = (await multi.ReadAsync<GetAllEventDTO>()).ToList();
//            var totalRecords = await multi.ReadSingleAsync<int>();

//            var response = new PagedResponse<List<GetAllEventDTO>>(
//                          events,
//                          request.PageNumber,
//                          request.PageSize,
//                          (int)Math.Ceiling((double)totalRecords / request.PageSize),
//                          totalRecords
//             );

//            return response;
//        }

//        public async Task<PagedResponse<List<GetAllEventDTO>>> GetEventByUser(PagedRequest request, Guid userId, string? title, bool? isDeleted)
//        {
//            using var connection = connectionFactory.GetOpenConnection();
            
//            const string sqlQuery =
//                          @"Select title AS Title, description AS Description,
//                                      start_time AS StartTime, end_time AS EndTime,
//                                      province AS Province, address AS Address,
//                                      additional_info AS AdditionalInfo,
//                                      event_type_id AS EventTypeID,
//                                      is_deleted AS IsDeleted,
//                                      user_id AS UserID
//                                      From events
//                                      WHERE user_id = @userID 
//                                      AND (@Title IS NULL OR title LIKE '%' + @Title + '%')
//                                      AND (@IsDeleted IS NULL OR is_deleted = @IsDeleted)
//                                      ORDER BY id
//                                      OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY
                    
//                                      SELECT COUNT(*) FROM events
//                                      WHERE user_id = @userID 
//                                      AND(@Title IS NULL OR title LIKE '%' + @Title + '%')
//                                      AND (@IsDeleted IS NULL OR is_deleted = @IsDeleted);
//                                    ";
//            var paramenter = new
//            {
//                UserID = userId,
//                Title = title,
//                ISDeleted = isDeleted,
//                request.PageSize,
//                Offset = (request.PageNumber - 1) * request.PageSize
//            };
//            using var multi = await connection.QueryMultipleAsync(sqlQuery,paramenter);
//            var listEvent = (await multi.ReadAsync<GetAllEventDTO>()).ToList();
//            var totalRecords = await multi.ReadSingleAsync<int>();

//            return new PagedResponse<List<GetAllEventDTO>>(
//                listEvent,
//                request.PageNumber,
//                request.PageSize,
//                (int)Math.Ceiling((double)totalRecords / request.PageSize),
//                totalRecords
//                );
//        }

//        public async Task<GetEventDetailDTO> GetEventDetail(Guid eventID)
//        {
//            using var connection = connectionFactory.GetOpenConnection();
//            using var httpClient = new HttpClient();
//            const string query =
//                                    @"SELECT 
//                                        e.title AS Title, e.description AS Description, 
//                                        e.start_time AS StartTime, e.end_time AS EndTime, 
//                                        e.province AS Province, e.address AS Address, 
//                                        e.additional_info AS AdditionalInfo, 
//                                        e.is_deleted AS IsDeleted,
//                                        e.event_type_id AS EventTypeID, e.user_id AS UserID
//                                        FROM Events e
//                                        WHERE e.id = @eventID;";

//            var eventData = connection.QueryFirstOrDefault<Events>(query, new {eventID});

//            if (eventData == null)
//            {
//                throw new Exception("Event not found!");
//            }

//            var userApiResponse = await httpClient.GetStringAsync($"{_configuration["BaseURL:UserService"]}/users/{eventData.UserID}");
//            using var jsonDoc = JsonDocument.Parse(userApiResponse);
//            dynamic userData = JsonSerializer.Deserialize<dynamic>(jsonDoc.RootElement.GetProperty("data").ToString())!;


//            var eventTypeApiResponse = await httpClient.GetStringAsync($"{_configuration["BaseURL:EventService"]}/event-type/get-by-id?EventTypeID={eventData.EventTypeID}");

//            var eventTypeResponse = JsonSerializer.Deserialize<APIResponseDTO<GetEvenTypeDTO>>(eventTypeApiResponse);

//            if (eventTypeResponse?.Data == null)
//            {
//                throw new Exception("Event type not found!");
//            }

//            return new GetEventDetailDTO
//            {
//                Title = eventData.Title,
//                Description = eventData.Description,
//                StartTime = eventData.StartTime,
//                EndTime = eventData.EndTime,
//                Province = eventData.Province,
//                Address = eventData.Address,
//                AdditionalInfo = eventData.AdditionalInfo,
//                IsDeleted = eventData.isDeleted,
//                eventType = eventTypeResponse.Data.Name,
//                FirstName = userData.GetProperty("firstName").GetString(),
//                LastName = userData.GetProperty("lastName").GetString(),
//                Username = userData.GetProperty("username").GetString(),
//                Email = userData.GetProperty("email").GetString(),
//                IsEmailConfirmed = userData.GetProperty("isEmailConfirmed").GetBoolean(),
//                Phone = userData.GetProperty("phone").GetString(),
//                IsPhoneConfirmed = userData.GetProperty("phoneConfirmed").GetBoolean(),
//                IsActive = userData.GetProperty("isActive").GetBoolean(),
//                CreatedAt = userData.GetProperty("createAt").GetDateTime(),
//                Gender = userData.GetProperty("gender").GetInt32()
//            };
//        }
//    }
//}
