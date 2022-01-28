using System;

namespace api_csharp.Responses
{
    public class UserResponse
    {
        public string FirstName { get; set; }

        public string SurName { get; set; }

        public int Age { get; set; }

        public DateTime CreationDate { get; set; }
    }
}