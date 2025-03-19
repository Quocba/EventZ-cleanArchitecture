using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Application.Interface;
using Event.Application.Wrappers;
using MassTransit;
using MassTransit.Clients;
using MediatR;
using RabbitMQ.Contract.DTO;

namespace Event.Application.Feature.SumaryEvent.Queries.TotalNumberOfAttendees
{
    public class TotalNumberOfAttendeesQueryHandle(IEventBookingQuery _eventBookingQuery, IRequestClient<GetUserRequestDTO> requestClient)
        : IRequestHandler<TotalNumberOfAttendeesQuery, PagedResponseWithTotal<List<TotalNumberOfAttendessDTO>>>
    {
        public async Task<PagedResponseWithTotal<List<TotalNumberOfAttendessDTO>>> Handle(TotalNumberOfAttendeesQuery request, CancellationToken cancellationToken)
        {
            var result = await _eventBookingQuery.GetTotalNumberAddtendess(
                new PagedRequest { PageNumber = request.pageNUmber, PageSize = request.pageSize },
                request.EventID);

            var userList = new List<GetUserResponseDTO>();

            var userTasks = result.Data.Select(async booking =>
            {
                var response = await requestClient.GetResponse<GetUserResponseDTO>(new GetUserRequestDTO { ID = booking.ID });
                if (response?.Message != null)
                {
                    lock (userList)
                    {
                        userList.Add(response.Message);
                    }
                }
            });

            await Task.WhenAll(userTasks);

            var mappedData = result.Data.Select(booking =>
            {
                var user = userList.FirstOrDefault(u => u.Id == booking.ID);
                return new TotalNumberOfAttendessDTO
                {
                    EventBookingID = booking.EventBookingID,
                    NumSeat = booking.NumSeat,
                    AdditionalInfo = booking.AdditionalInfo,
                    IsActive = booking.IsActive,
                    Status = booking.Status,
                    CreatedAt = booking.CreatedAt,
                    UpdatedAt = booking.UpdatedAt,
                    EventSeatID = booking.EventSeatID,
                    UserID = booking.ID,
                    PaymentID = booking.PaymentID,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Phone = user.Phone
                };
            }).ToList();

            return new PagedResponseWithTotal<List<TotalNumberOfAttendessDTO>>(
                       mappedData,
                       request.pageNUmber,
                       request.pageSize,
                       result.TotalRecord,
                       mappedData.Count,
                       result.TotalRecord 
             );

        }

    }
}
