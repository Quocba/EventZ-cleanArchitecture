using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using RabbitMQ.Contract.DomainEvents;
using RabbitMQ.Contract.DTO;

namespace Event.Application.MessageBus
{
    public class SendMailInviteUserConsumer(ISender _sender, IConfiguration _configuration, IEmailSender _emailSender) : IConsumer<SendMailInviteUserDTO>
    {
        public async Task Consume(ConsumeContext<SendMailInviteUserDTO> context)
        {
            try
            {
                var emailToken = GenerateEmailConfirmationToken(context.Message.Email);
                var confirmationLink = $"{_configuration["ClientURL:URL"]}{emailToken}";

                var emailBody = GenerateEmailBody(context.Message, confirmationLink);

                await _emailSender.SendEmailAsync(context.Message.Email, "Confirm your participation in the event", emailBody);

                await context.RespondAsync(new SendMailInviteUserResponse
                {
                    Success = true,
                    Message = "Success",
                    Token = emailToken,
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.ToString()}");

                await context.RespondAsync(new SendMailInviteUserResponse
                {
                    Success = false,
                    Message = "FAIL"
                });
            }
        }

        private string GenerateEmailConfirmationToken(string email)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Email, email),
                new Claim("Purpose", "EmailConfirmation"),
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private string GenerateEmailBody(SendMailInviteUserDTO request, string confirmationLink)
        {
            return $@"
                <h3>Thư mời tham gia sự kiện</h3>
                <p>Xin chào,</p>
                <p>Bạn được mời tham gia sự kiện <strong>{request.EventTItle}</strong>.</p>
                <p>Nội dung: {request.Content}</p>
                <p>Vui lòng xác nhận tham gia bằng cách nhấn vào liên kết bên dưới:</p>
                <p><a href='{confirmationLink}'>Xác nhận tham gia</a></p>
                <p>Trân trọng!</p>";
        }

    }
}
