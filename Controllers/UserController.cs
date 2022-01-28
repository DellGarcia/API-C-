using api_csharp.Services;
using api_csharp.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace api_csharp.Controllers
{
    public class UserController : ControllerBase
    {
        protected UserService service;

        public UserController(
            UserService service)
        {
            this.service = service;
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteById(Guid id)
        {
            var response = await service.DeleteById(id);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(Guid id)
        {
            var response = await service.GetById(id);
            return Ok(response);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post(UserRequest request)
        {
            var response = await service.Create(request);
            return Ok(response);
        }

        [HttpPut]
        public virtual async Task<IActionResult> Put(UserRequest request)
        {
            var response = await service.Update(request);
            return Ok(response);
        }
    }
}