using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EventProduct.Application.DTO
{
    public class APIResponseDTO<T>
    {
        public String StatusCode { get; set; }
        public bool Success { get; set; }
        public T Data { get; set; }
        public object Errors { get; set; }
        public string Message { get; set; }
    }
}
