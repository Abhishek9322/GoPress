using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPress.Application.Features.Orders.Responses
{
    public class Response<T>
    {
        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }

        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public bool Succeeded { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }
    }
}
