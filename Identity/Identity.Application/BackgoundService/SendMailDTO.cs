using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.BackgoundService
{
    public class SendMailDTO
    {
       public string Email { get; set; }
       public String UserName { get; set; }
       public String EventTitle {  get; set; }
       public string StartTime {  get; set; }
       public string Address {  get; set; }
    }
}
