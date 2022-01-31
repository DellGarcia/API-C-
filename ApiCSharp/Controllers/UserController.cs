using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<IEnumerable<User>> GetUser()
        {
            _logger.LogInformation($"Executando GET em api/user");
            var result = _context.User.ToList();
            _logger.LogInformation($"Foram obtidos {result.Count} usuários");
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
                _logger.LogWarning($"Não existe registro de um Usuário com Id={id}");
                return NotFound(new { error = "Registro não encontrado" });
            }

            _logger.LogInformation($"Usuário Id={id} encontrado");
            return user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            _logger.LogInformation($"Executando PUT em api/user/{id}");
            
            if (!UserExists(id))
            {
                _logger.LogWarning($"Não existe registro de um Usuário com Id={id}");
                return NotFound(new { error = "Registro não encontrado" });
            }

            if (!IsRequiredFieldsCorrectlyFilled(user))
                return BadRequest(new { 
                    error = "Os campos não foram preenchidos corretamente", 
                    detail = "firstName e idade são obrigatórios e a idade deve ser superior a 12"
                });

            if(IsInvalidFieldsFilled(user))
                return BadRequest(new { error = "Requisição inválida" });

            user.Id = id;

            _context.Update(user)
                .Property(p => p.CreationDate)
                .IsModified = false;
            
            _logger.LogInformation($"Verificando dados para salvar");
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Atualização efetuada com sucesso para o User com ID={id}");
            return NoContent();
        }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _logger.LogInformation($"Executando POST para api/user");

            if(IsInvalidFieldsFilled(user))
                return BadRequest(new { error = "Requisição inválida" });

            if (!IsRequiredFieldsCorrectlyFilled(user))
                return BadRequest(new { 
                    error = "Os campos não foram preenchidos corretamente", 
                    detail = "firstName e idade são obrigatórios e a idade deve ser superior a 12"
                });

            user.CreationDate = DateTime.Now;

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Registro criado com sucesso");
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
                _logger.LogWarning($"Não existe registro de um Usuário com Id={id}");
                return NotFound(new { error = "Registro não encontrado" });
            }

            _logger.LogInformation("Iniciando tentativa de remoção");

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"O usuário de Id={id} foi removido com sucesso");
            return NoContent();
        }

        private bool UserExists(Guid id)
        {
            return _context.User.Any(e => e.Id == id);
        }

        private static bool IsRequiredFieldsCorrectlyFilled(User user)
        {
            return user.FirstName != null && user.Age > 12 && user.Age < 200;
        }

        private static bool IsInvalidFieldsFilled(User user) {
            return user.Id != null || user.CreationDate != null;
        }
    }
}
