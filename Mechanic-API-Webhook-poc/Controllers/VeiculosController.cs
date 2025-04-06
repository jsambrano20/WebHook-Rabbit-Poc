using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mechanic_API_Webhook_poc.Domain.Entities;
using Mechanic_API_Webhook_poc.Infra.Data;
using Mechanic_API_Webhook_poc.Domain.Dto;
using MassTransit;
using Mechanic_API_Webhook_poc.Domain.Dto.Event;

namespace Mechanic_API_Webhook_poc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeiculosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IPublishEndpoint _publishEndpoint;

        public VeiculosController(AppDbContext context,
            IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
        }

        // GET: api/veiculos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EntityVeiculo>>> GetAll()
        {
            var veiculos = await _context.Veiculos.ToListAsync();
            return Ok(veiculos);
        }

        // GET: api/veiculos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EntityVeiculo>> GetById(int id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);

            if (veiculo == null)
                return NotFound();

            return Ok(veiculo);
        }

        // POST: api/veiculos
        [HttpPost]
        public async Task<ActionResult<EntityVeiculo>> Create([FromBody] EntityVeiculo entityVeiculo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Veiculos.Add(entityVeiculo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = entityVeiculo.Id }, entityVeiculo);
        }

        // PUT: api/veiculos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] VeiculoUpdate entityVeiculo)
        {
            if (id != entityVeiculo.Id)
                return BadRequest("ID da URL não corresponde ao objeto enviado.");

            var veiculoAntes = await _context.Veiculos.FindAsync(id);
            var veiculoExistente = await _context.Veiculos.FindAsync(id);
            if (veiculoExistente == null)
                return NotFound();

            var statusAnterior = veiculoExistente.Status;

            // Atualiza apenas os campos permitidos
            veiculoExistente.Status = entityVeiculo.Status;
            veiculoExistente.Comentario = entityVeiculo.Comentario;
            veiculoExistente.ValorOrcamento = entityVeiculo.ValorOrcamento;

            await _context.SaveChangesAsync();

            if (veiculoExistente.Status != statusAnterior)
            {
                await _publishEndpoint.Publish<IStatusVeiculoAlteradoEvent>(new
                {
                    VeiculoId = veiculoExistente.Id,
                    StatusAnterior = statusAnterior.GetDescription(),
                    StatusAtual = veiculoExistente.Status.GetDescription(),
                    DataAlteracao = DateTime.UtcNow
                });
            }

            return NoContent();
        }

        // DELETE: api/veiculos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);
            if (veiculo == null)
                return NotFound();

            _context.Veiculos.Remove(veiculo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
