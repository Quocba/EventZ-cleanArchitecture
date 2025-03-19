﻿using Payment.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Application.Interfaces
{
    public interface IVNPayService
    {
        string GetLink(string userId, string content, decimal amount, string returnURL);
        bool IsValidSignature(VnpayPayResponse data);
        string GetClientReturnURL();
    }
}
