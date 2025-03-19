using MassTransit;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using RabbitMQ.Contract.DomainEvents;
using RabbitMQ.Contract.DTO;

namespace EmailService.MessageBus
{
    public class SendEmailConsumer(IEmailSender _emailSender) : IConsumer<SendEmailEvent>
    {
        public async Task Consume(ConsumeContext<SendEmailEvent> context)
        {
            try
            {
                await _emailSender.SendEmailAsync(context.Message.Email, context.Message.Subject, context.Message.HtmlMessage);

                await context.RespondAsync(new SendEmailResponse { IsSuccess = true });
            }
            catch (Exception ex)
            {
                await context.RespondAsync(new SendEmailResponse { IsSuccess = false, ErrorMessage = ex.Message });
            }
        }
    }
}
