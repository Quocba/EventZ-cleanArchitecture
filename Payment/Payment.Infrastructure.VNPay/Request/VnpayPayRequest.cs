using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Payment.Infrastructure.VNPay.Helpers;
using Payment.Infrastructure.VNPay.Lib;

namespace Payment.Infrastructure.VNPay.Request
{
    public class VnpayPayRequest
    {
        public SortedList<string, string> requestData = new(new VnpayCompare());
        public VnpayPayRequest() { }
        public VnpayPayRequest(string version, string tmnCode, string createDate, string expireDate, string ipAddress,
            decimal amount, string currCode, string orderType, string orderInfo,
            string returnUrl, string txnRef, string locale, string command)
        {
            Vnp_Locale = locale;
            Vnp_IpAddr = ipAddress;
            Vnp_Version = version;
            Vnp_CurrCode = currCode;
            Vnp_CreateDate = createDate;
            Vnp_ExpireDate = expireDate;
            Vnp_TmnCode = tmnCode;
            Vnp_Amount = (int)amount * 100;
            Vnp_Command = command;
            Vnp_OrderType = orderType;
            Vnp_OrderInfo = orderInfo;
            Vnp_ReturnUrl = returnUrl;
            Vnp_TxnRef = txnRef;
        }

        public string GetLink(string baseUrl, string secretKey)
        {
            MakeRequestData();
            StringBuilder data = new StringBuilder();
            foreach(KeyValuePair<string, string> kv in requestData)
            {
                if(!string.IsNullOrEmpty(kv.Value))
                {
                    data.Append(WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&");
                }
            }

            string result = baseUrl + "?" + data.ToString();
            var secureHash = HashHelper.HmacSHA512(secretKey, data.ToString().Remove(data.Length - 1, 1));
            return result += "vnp_SecureHash=" + secureHash;
        }

        public void MakeRequestData()
        {
            if (Vnp_Amount != null)
                requestData.Add("vnp_Amount", Vnp_Amount.ToString() ?? string.Empty);
            if (Vnp_Command != null)
                requestData.Add("vnp_Command", Vnp_Command);
            if (Vnp_CreateDate != null)
                requestData.Add("vnp_CreateDate", Vnp_CreateDate);
            if (Vnp_CurrCode != null)
                requestData.Add("vnp_CurrCode", Vnp_CurrCode);
            if (Vnp_BankCode != null)
                requestData.Add("vnp_BankCode", Vnp_BankCode);
            if (Vnp_IpAddr != null)
                requestData.Add("vnp_IpAddr", Vnp_IpAddr);
            if (Vnp_Locale != null)
                requestData.Add("vnp_Locale", Vnp_Locale);
            if (Vnp_OrderInfo != null)
                requestData.Add("vnp_OrderInfo", Vnp_OrderInfo);
            if (Vnp_OrderType != null)
                requestData.Add("vnp_OrderType", Vnp_OrderType);
            if (Vnp_ReturnUrl != null)
                requestData.Add("vnp_ReturnUrl", Vnp_ReturnUrl);
            if (Vnp_TmnCode != null)
                requestData.Add("vnp_TmnCode", Vnp_TmnCode);
            if (Vnp_ExpireDate != null)
                requestData.Add("vnp_ExpireDate", Vnp_ExpireDate);
            if (Vnp_TxnRef != null)
                requestData.Add("vnp_TxnRef", Vnp_TxnRef);
            if (Vnp_Version != null)
                requestData.Add("vnp_Version", Vnp_Version);
        }
        public decimal? Vnp_Amount { get; set; }
        public string? Vnp_Command { get; set; }
        public string? Vnp_CreateDate { get; set; }
        public string? Vnp_CurrCode { get; set; }
        public string? Vnp_BankCode { get; set; }
        public string? Vnp_IpAddr { get; set; }
        public string? Vnp_Locale { get; set; }
        public string? Vnp_OrderInfo { get; set; }
        public string? Vnp_OrderType { get; set; }
        public string? Vnp_ReturnUrl { get; set; }
        public string? Vnp_TmnCode { get; set; }
        public string? Vnp_ExpireDate { get; set; }
        public string? Vnp_TxnRef { get; set; }
        public string? Vnp_Version { get; set; }
        public string? Vnp_SecureHash { get; set; }
    }
}
