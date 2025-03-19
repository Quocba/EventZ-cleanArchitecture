using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Application.Interfaces;
using MassTransit;
using MediatR;
using RabbitMQ.Contract.DomainEvents;

namespace Event.Application.Feature.EventTimeLine.Command.SendMailTimeLine
{
    public class SenMailTimeLineCommandHandle(IRepository<Domain.Entities.EventTimeLine> _eventTimeLineRepository, IPublishEndpoint _publish) : IRequestHandler<SendMailTimeLineCommand>
    {
        public async Task Handle(SendMailTimeLineCommand request, CancellationToken cancellationToken)
        {
            var getTimeLine = await _eventTimeLineRepository.GetByIdAsync(request.TimeLineID);
            if (getTimeLine == null) {

                throw new NotImplementedException();
            }

            var message = new SendEmailEvent
            {
                Email = getTimeLine.HandleBy.Email,
                Subject = "[EventZ Bạn Có 1 Time Line]",
                HtmlMessage = GenerateEmailHtml(getTimeLine)
            };
            await _publish.Publish(message);

        }


        public static string GenerateEmailHtml(Domain.Entities.EventTimeLine eventTimeLine)
        {
            return $@"
        <!DOCTYPE html>
        <html lang='vi'>
        <head>
            <meta charset='UTF-8'>
            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            <title>Thông báo sự kiện</title>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    background-color: #f4f4f4;
                    margin: 0;
                    padding: 0;
                }}
                .container {{
                    max-width: 600px;
                    background: #ffffff;
                    margin: 20px auto;
                    padding: 20px;
                    border-radius: 10px;
                    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                    text-align: center;
                }}
                .header, .footer {{
                    background: linear-gradient(135deg, #3a0ca3, #4361ee);
                    color: #ffffff;
                    padding: 15px;
                    border-radius: 10px 10px 0 0;
                    text-align: center;
                    font-size: 20px;
                }}
                .footer {{
                    border-radius: 0 0 10px 10px;
                    font-size: 14px;
                }}
                .logo {{
                    max-width: 80px;
                    display: block;
                    margin: 0 auto 10px;
                }}
                .content {{
                    padding: 20px;
                    text-align: left;
                    background: #ffffff;
                    border-radius: 10px;
                }}
                .content p {{
                    background: #eef1ff;
                    padding: 10px;
                    border-radius: 5px;
                    color: #333;
                }}
            </style>
        </head>
        <body>
            <div class='container'>
                <div class='header'>
                    <img src='https://4lua.sgp1.digitaloceanspaces.com/EventZ/970a4b7f-baa8-402a-929d-8802839cb235_PNG-04.png' 
                        alt='Event Logo' class='logo'>
                    <div>📢 EventZ Thông Báo</div>
                </div>
                <div class='content'>
                    <p><strong>📌 Tiêu đề:</strong> {eventTimeLine.Title}</p>
                    <p><strong>📜 Nội dung:</strong> {eventTimeLine.Content}</p>
                    <p><strong>📅 Thời gian bắt đầu:</strong> {eventTimeLine.StartDate:dd/MM/yyyy HH:mm}</p>
                    <p><strong>⏳ Thời gian kết thúc:</strong> {eventTimeLine.EndDate:dd/MM/yyyy HH:mm}</p>
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
