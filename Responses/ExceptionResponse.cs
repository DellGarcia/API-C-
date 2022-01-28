using System.Net;

namespace api_csharp.Responses
{
    public class ExceptionResponse
    {

        public ExceptionResponse(
            string message = null,
            HttpStatusCode code = HttpStatusCode.OK)
        {
            Success = true;
            Code = code;
            Message = message;
        }
        public HttpStatusCode Code { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }

        public string ExceptionName { get; set; }
    }
}