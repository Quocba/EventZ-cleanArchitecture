using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Identity.UI.Services;
using RabbitMQ.Contract.DomainEvents;

namespace Identity.Application.MessageBus
{
    public class SendMailConsumer(IEmailSender _mailSender) : IConsumer<SendEmailEvent>
    {
        public async Task Consume(ConsumeContext<SendEmailEvent> context)
        {
            var message = context.Message;
            try
            {
                await _mailSender.SendEmailAsync(message.Email, message.Subject, message.HtmlMessage);
                Console.WriteLine($"Email sent to: {message.Email}");
            }
            catch (Exception ex) {
                Console.WriteLine($"Failed to send email to {message.Email}: {ex.Message}");
            }
        }

    }
}
