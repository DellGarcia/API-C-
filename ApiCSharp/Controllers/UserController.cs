using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Api_CSharp.Database;
using Api_CSharp.Models;

namespace Api_CSharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, ApplicationDBContext context)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            _logger.LogInformation($"Executando GET em api/user");
            var result = await _context.User.ToListAsync();
            _logger.LogInformation($"Foram obtidos {result.Count} usu�rios");
            return result;
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            _logger.LogInformation($"Executando GET em api/user/{id}");
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                _logger.LogWarning($"N�o existe registro de um Usu�rio com Id={id}");
                return NotFound();
            }

            _logger.LogInformation($"Usu�rio Id={id} encontrado");
            return user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            user.Id = id;
            _logger.LogInformation($"Executando PUT em api/user/{id}");

            if (!IsRequiredFieldsCorrectlyFilled(user))
                return BadRequest();

            _context.Entry(user).State = EntityState.Modified;
            _context.Entry(user).Property(p => p.CreationDate).IsModified = false;

            try
            {
                _logger.LogInformation($"Verificando dados para salvar");
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    _logger.LogWarning($"N�o existe registro de um Usu�rio com Id={id}");
                    return NotFound();
                }
                else
                    throw;
            }

            _logger.LogInformation($"Atualiza��o efetuada com sucesso para o User com ID={id}");
            return NoContent();
        }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _logger.LogInformation($"Executando POST para api/user");

            if(!IsRequiredFieldsCorrectlyFilled(user))
                return BadRequest();

            user.CreationDate = null;
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"");
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            _logger.LogInformation($"Executando DELETE para api/users/${id}");
            var user = await _context.User.FindAsync(id);
            if (user == null)
            { 
                _logger.LogWarning($"N�o existe registro de um Usu�rio com Id={id}");
                return NotFound();
            }

            _logger.LogInformation("Iniciando tentativa de remo��o");
            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"O usu�rio de Id={id} foi removido com sucesso");
            return NoContent();
        }

        private bool UserExists(Guid id)
        {
            return _context.User.Any(e => e.Id == id);
        }

        private bool IsRequiredFieldsCorrectlyFilled(User user)
        {
            return user.FirstName != null && user.Age > 12 && user.Age < 200;
        }
    }
}
