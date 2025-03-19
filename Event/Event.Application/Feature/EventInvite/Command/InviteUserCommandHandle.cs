using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.DTO;
using Event.Application.Interfaces;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Contract.DomainEvents;
using RabbitMQ.Contract.DTO;

namespace Event.Application.Feature.EventInvite.Command
{
    public class InviteUserCommandHandle(IRepository<Domain.Entities.EventInvite> _eventInviteRepository,
        IRepository<Domain.Entities.Events> _eventRepository, IPublishEndpoint _publishEndpoint)
        : IRequestHandler<InviteUserCommand,Guid>
    {
        async Task<Guid> IRequestHandler<InviteUserCommand, Guid>.Handle(InviteUserCommand request, CancellationToken cancellationToken)
        {
            var checkAlready = await _eventInviteRepository.FindWithInclude()
                                                     .FirstOrDefaultAsync(x => x.Email.ToLower().Equals(request.Email.ToLower())
                                                     && x.EventID == request.EventID);
            if (checkAlready != null)
            {
                throw new Exception("Email already exist");
            }
            var inViteNewUser = new Domain.Entities.EventInvite
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                Content = request.Content,
                CreateAt = request.CreateAt,
                UpdateAt = request.UpdateAt,
                IsChecked = request.IsChecked,
                IsConfirm = request.IsConfirmed,
                Phone = request.Phone,
                Title = request.Title,
                EventID = request.EventID
            };;
            await _eventInviteRepository.AddAsync(inViteNewUser);
            await _eventInviteRepository.SaveAsync();
            await _publishEndpoint.Publish(new SendMailInviteUserDTO
            {
                Email = request.Email,
                Subject = "[EventZ][Thư mời tham dự sự kiện]",
                EventTItle = request.Title,
                Content = request.Content
            });
            return inViteNewUser.Id;
        }
    }
}
