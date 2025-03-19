using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Application.Features.PaymentEvent.Commands.PaymentEventReturn
{
    public class PaymentEventReturnCommand : IRequest<string>
    {
        public string Vnp_TmnCode { get; set; } = string.Empty;
        public string Vnp_BankCode { get; set; } = string.Empty;
        public string Vnp_BankTranNo { get; set; } = string.Empty;
        public string Vnp_CardType { get; set; } = string.Empty;
        public string Vnp_OrderInfo { get; set; } = string.Empty;
        public string Vnp_TransactionNo { get; set; } = string.Empty;
        public string Vnp_TransactionStatus { get; set; } = string.Empty;
        public string Vnp_TxnRef { get; set; } = string.Empty;
        public string Vnp_SecureHashType { get; set; } = string.Empty;
        public string Vnp_SecureHash { get; set; } = string.Empty;
        public int? Vnp_Amount { get; set; }
        public string? Vnp_ResponseCode { get; set; }
        public string Vnp_PayDate { get; set; } = string.Empty;
    }
}
