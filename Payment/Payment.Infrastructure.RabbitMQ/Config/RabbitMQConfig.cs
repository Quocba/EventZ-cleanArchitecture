using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Infrastructure.RabbitMQ.Config
{
    public class RabbitMQConfig
    {
        public static string ConfigName => "RabbitMQ";
        public string Host { get; set; }
        public string VHost { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
