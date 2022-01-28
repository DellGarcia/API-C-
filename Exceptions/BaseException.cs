using System;
using System.Net;

namespace api_csharp.Exceptions
{
    public class BaseException : Exception
    {
        public HttpStatusCode Code { get; set; }
        public dynamic Response { get; set; }

        public BaseException() { }

        public BaseException(dynamic response, HttpStatusCode code)
        {
            Response = response;
            Code = code;
        }

        public BaseException(dynamic response)
        {
            Response = response;
            Code = HttpStatusCode.BadRequest;
        }
    }
}
