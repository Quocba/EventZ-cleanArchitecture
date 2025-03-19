using MediatR;
using Microsoft.Extensions.Options;
using Payment.Application.DTO;
using Payment.Application.Interfaces;
using Payment.Domain.Shares;
using Payment.Infrastructure.VNPay.Config;
using Payment.Infrastructure.VNPay.Helpers;
using Payment.Infrastructure.VNPay.Lib;
using Payment.Infrastructure.VNPay.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Infrastructure.VNPay.Services
{
    public class VNPayService(IOptions<VnpayConfig> vnpayConfigOptions) : IVNPayService
    {
        private readonly VnpayConfig _vnpayConfig = vnpayConfigOptions.Value;

        public string GetLink(string userId, string content, decimal amount, string returnURL)
        {
            DateTime currentTimeInZone7 = DateUtility.GetCurrentDateTime();

            var version = _vnpayConfig.Version;
            var tmnCode = _vnpayConfig.TmnCode;
            var locale = _vnpayConfig.Locale;
            var command = _vnpayConfig.Command;
            var createDate = currentTimeInZone7.ToString("yyyyMMddHHmmss");
            var expireDate = currentTimeInZone7.AddMinutes(15).ToString("yyyyMMddHHmmss");
            var paymentCurrency = _vnpayConfig.Currency;

            returnURL = _vnpayConfig.ReturnUrl + returnURL;

            var vnpayPayRequest = new VnpayPayRequest(version, tmnCode
                                , createDate, expireDate, userId, amount, paymentCurrency,
                                "other", content, returnURL, createDate, locale, command);

            var paymentUrl = vnpayPayRequest.GetLink(_vnpayConfig.PaymentUrl, _vnpayConfig.HashSecret);

            return paymentUrl;
        }

        public bool IsValidSignature(VnpayPayResponse data)
        {
            var responseData = MakeResponseData(data);
            StringBuilder stringBuider = new();
            foreach (KeyValuePair<string, string> kv in responseData)
            {
                if (!string.IsNullOrEmpty(kv.Value))
                {
                    stringBuider.Append(WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&");
                }
            }
            string checkSum = HashHelper.HmacSHA512(_vnpayConfig.HashSecret,
                stringBuider.ToString().Remove(stringBuider.Length - 1, 1));
            return checkSum.Equals(data.Vnp_SecureHash, StringComparison.InvariantCultureIgnoreCase);
        }

        private static SortedList<string, string> MakeResponseData(VnpayPayResponse data)
        {
            SortedList<string, string> responseData = new(new VnpayCompare());
            if (data.Vnp_Amount != null)
                responseData.Add("vnp_Amount", data.Vnp_Amount.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(data.Vnp_TmnCode))
                responseData.Add("vnp_TmnCode", data.Vnp_TmnCode.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(data.Vnp_BankCode))
                responseData.Add("vnp_BankCode", data.Vnp_BankCode.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(data.Vnp_BankTranNo))
                responseData.Add("vnp_BankTranNo", data.Vnp_BankTranNo.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(data.Vnp_CardType))
                responseData.Add("vnp_CardType", data.Vnp_CardType.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(data.Vnp_OrderInfo))
                responseData.Add("vnp_OrderInfo", data.Vnp_OrderInfo.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(data.Vnp_TransactionNo))
                responseData.Add("vnp_TransactionNo", data.Vnp_TransactionNo.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(data.Vnp_TransactionStatus))
                responseData.Add("vnp_TransactionStatus", data.Vnp_TransactionStatus.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(data.Vnp_TxnRef))
                responseData.Add("vnp_TxnRef", data.Vnp_TxnRef.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(data.Vnp_PayDate))
                responseData.Add("vnp_PayDate", data.Vnp_PayDate.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(data.Vnp_ResponseCode))
                responseData.Add("vnp_ResponseCode", data.Vnp_ResponseCode ?? string.Empty);

            return responseData;
        }

        public string GetClientReturnURL()
        {
            return _vnpayConfig.ClentReturnUrl;
        }
    }
}
