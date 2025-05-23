﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Infrastructure.VNPay.Config
{
    public class VnpayConfig
    {
        public static string ConfigName => "Vnpay";
        public string Version { get; set; } = string.Empty;
        public string TmnCode { get; set; } = string.Empty;
        public string HashSecret { get; set; } = string.Empty;
        public string ReturnUrl { get; set; } = string.Empty;
        public string ClentReturnUrl { get; set; } = string.Empty;
        public string PaymentUrl { get; set;} = string.Empty;
        public string Command { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public string Locale { get; set; } = string.Empty;
    }
}
