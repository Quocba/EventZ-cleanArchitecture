﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Application.Wrappers
{
    public class Response<T>
    {
        public Response()
        {
        }
        public Response(T data)
        {
            Data = data;
        }
        public T Data { get; set; }
    }
}
