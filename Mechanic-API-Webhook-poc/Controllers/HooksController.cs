using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mechanic_API_Webhook_poc.Domain.Entities;
using Mechanic_API_Webhook_poc.Infra.Data;

namespace Mechanic_API_Webhook_poc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HooksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HooksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/hooks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EntityVeiculo>>> GetAll()
        {
            var veiculos = await _context.Veiculos.ToListAsync();
            return Ok(veiculos);
        }

        // GET: api/hooks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EntityVeiculo>> GetById(int id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);

            if (veiculo == null)
                return NotFound();

            return Ok(veiculo);
        }

        // POST: api/hooks
        [HttpPost]
        public async Task<ActionResult<EntityVeiculo>> Create([FromBody] EntityVeiculo entityVeiculo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Veiculos.Add(entityVeiculo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = entityVeiculo.Id }, entityVeiculo);
        }

        // PUT: api/hooks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EntityVeiculo entityVeiculo)
        {
            if (id != entityVeiculo.Id)
                return BadRequest("ID na URL diferente do corpo da requisição");

            _context.Entry(entityVeiculo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Veiculos.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }
    }
}
