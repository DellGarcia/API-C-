using System;
using System.Net;
using api_csharp.Exceptions;
using api_csharp.Responses;

namespace api_csharp.Exceptions
{
    public class RegisterNotFoundException : BaseException
    {
        public RegisterNotFoundException(string entity, Guid id)
        {
            Code = HttpStatusCode.NotFound;
            Response = new ExceptionResponse()
            {
                Success = false,
                Code = Code,
                ExceptionName = nameof(RegisterNotFoundException),
                Message = $"O registro na entidade ({entity}), de ID ({id}) não existe!"
            };
        }
    }
}

