using  System;
using System.Text.Json.Serialization;

namespace api_csharp.Requests
{
    public class UserRequest
    {
        public string FirstName { get; set; }

        public string SurName { get; set; }

        public int? Age { get; set; }

        [JsonIgnore]
        public DateTime CreationDate { get; set; } 
    }
}