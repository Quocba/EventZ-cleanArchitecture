using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading;
using Identity.Application.DTO;
using Identity.Application.Interfaces;
using Identity.Domain.Entities;
using Identity.Domain.Shares;
using Identity.Infrastructure.BackgoundService;
using MassTransit;
using MassTransit.Clients;
using MassTransit.Initializers;
using MassTransit.Internals;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Contract.DomainEvents;
using RabbitMQ.Contract.DTO;

namespace Identity.Application.Feature.BackGroundService
{
    public class NotificationService : IHostedService, IDisposable
    {
        private Timer? _timer;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<NotificationService> _logger;
        private readonly HttpClient _client;

        public NotificationService(IServiceScopeFactory scopeFactory, IConfiguration configuration,
            ILogger<NotificationService> logger, HttpClient httpClient)
        {
            _scopeFactory = scopeFactory;
            _configuration = configuration;
            _logger = logger;
            _client = httpClient;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            //var now = DateTime.Now;
            //var nextRun = now.Date.AddHours(8);
            //if (now > nextRun)
            //{
            //    nextRun = nextRun.AddDays(1);
            //}

            //var initialDelay = nextRun - now;

            //_logger.LogInformation($"Next execution at: {nextRun}");

            //_timer = new Timer(async _ =>
            //{
            //    _logger.LogInformation($"Executing task at: {DateTime.Now}");
            //    await NotifyEmailUserByEvent();

            //    _timer?.Change(TimeSpan.FromDays(1), TimeSpan.FromDays(1));
            //},
            //null,
            //initialDelay, Timeout.InfiniteTimeSpan);
            _logger.LogInformation("Notification Service Running");
            _timer = new Timer(async async => await NotifyEmailUserByEvent(), null, TimeSpan.Zero, TimeSpan.FromDays(30));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Notification Service Stopped");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        private async Task NotifyEmailUserByEvent()
        {
            using var scope = _scopeFactory.CreateScope();
            var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();
            var _cache = scope.ServiceProvider.GetRequiredService<IMemoryCache>();
            var reqeustClient = scope.ServiceProvider.GetRequiredService<IRequestClient<GetUpComingEventRequest>>();
            var _eventUserRepository = scope.ServiceProvider.GetRequiredService<IRepository<Domain.Entities.UserEvent>>();
            var today = DateUtility.GetCurrentDateTime();

            var response = await reqeustClient.GetResponse<GetUpcomingEventResponse>(new GetUpComingEventRequest());
            var listEvent = response.Message.Events;
            var listEventID = listEvent.Select(x => x.Id).ToList();

            var eventUserMap = await GetEventUserMap(_eventUserRepository, listEventID);
            foreach (var kvp in eventUserMap)
            {
                var eventObject = listEvent.FirstOrDefault(x => x.Id == kvp.Key);
                foreach (var userEvent in kvp.Value)
                {
                    var message = new SendEmailEvent
                    {
                        Email = userEvent.Email,
                        Subject = $"[EventZ Sự kiện {eventObject.Title} sắp diễn ra]",
                        HtmlMessage = GenerateEmailContent(eventObject, userEvent)
                    };
                    await publishEndpoint.Publish(message);
                    _logger.LogInformation($"Send Email Notification Event {eventObject.Title} to {userEvent.Email}");
                }
            }
        }

        private async Task<Dictionary<Guid, List<UserInfo>>> GetEventUserMap(
        IRepository<Domain.Entities.UserEvent> _eventUserRepository, List<Guid> listEventID)
        {
            var userEventGroup = await _eventUserRepository.FindWithInclude(ue => ue.User)
                .Where(ue => listEventID.Contains(ue.EventId))
                .Select(ue => new
                {
                    EventID = ue.EventId,
                    User = new UserInfo
                    {
                        Id = ue.UserId,
                        FirstName = ue.User.FirstName,
                        LastName = ue.User.LastName,
                        Email = ue.User.Email,
                        Phone = ue.User.Phone,
                        IsActive = ue.User.IsActive
                    }
                })
                .ToListAsync();
            var response = userEventGroup
                .GroupBy(ue => ue.EventID)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(ue => ue.User).DistinctBy(u => u.Id).ToList());

            return response;
        }
        private string GenerateEmailContent(UpComingEventDTO events, UserInfo users)
        {
            return $@"
<!DOCTYPE html>
<html lang='vi'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Nhắc nhở sự kiện</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }}
        .container {{
            max-width: 600px;
            background: #f7f7f7;;
            margin: 20px auto;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            text-align: center;
        }}
        .header {{
            background: linear-gradient(135deg, #3a0ca3, #4361ee);
            color: #ffffff;
            padding: 15px;
            text-align: center;
            font-size: 20px;
            border-radius: 10px 10px 0 0;
        }}
        .logo {{
            max-width: 100px;
            display: block;
            margin: 0 auto 10px;
            filter: drop-shadow(0px 0px 5px rgba(255, 255, 255, 0.7));
        }}
                .content {{
                    padding: 20px;
                    text-align: left;
                    background: #ffffff;
                    border-radius: 10px;
                }}
                .content ul {{
                    list-style-type: none;
                    padding: 0;
                }}
                .content ul li {{
                    background: #eef1ff;
                    padding: 10px;
                    margin-bottom: 8px;
                    border-radius: 5px;
                    color: #333;
                }}
        .footer {{
            text-align: center;
            padding: 10px;
            font-size: 14px;
            background: linear-gradient(135deg, #3a0ca3, #4361ee);
            color: #ffffff;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <img src=""https://4lua.sgp1.digitaloceanspaces.com/EventZ/970a4b7f-baa8-402a-929d-8802839cb235_PNG-04.png"" alt=""Event Logo"" class=""logo"">
            <div>Nhắc nhở sự kiện sắp tới</div>
        </div>
        <div class='content'>
            <p>Chào <strong>{users.FirstName + users.LastName}</strong>,</p>
            <p>Bạn có một sự kiện sắp diễn ra:</p>
            <ul>
                <li><strong>Sự kiện:</strong> {events.Title}</li>
                <li><strong>Thời gian:</strong> {events.StartTime:dd/MM/yyyy HH:mm}</li>
                <li><strong>Địa điểm:</strong> {events.Address}</li>
            </ul>
            <p>Hãy sắp xếp thời gian để tham dự nhé!</p>
        </div>
        <div class='footer'>
            <p>Trân trọng,</p>
            <p><strong>Đội ngũ tổ chức sự kiện</strong></p>
        </div>
    </div>
</body>
</html>";
        }

    }
}
