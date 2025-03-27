using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T5.PR1.Practica_1.Data;
using T5.PR1.Practica_1.Model;

namespace T4.PR1.Practica_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnergyIndicatorsController : ControllerBase
    {
        private readonly EcoEnergyDbContext _context;

        public EnergyIndicatorsController(EcoEnergyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnergyIndicatorBD>>> GetEnergyIndicators()
        {
            return await _context.EnergyIndicators.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EnergyIndicatorBD>> GetEnergyIndicator(int id)
        {
            var item = await _context.EnergyIndicators.FindAsync(id);
            if (item == null) return NotFound();
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<EnergyIndicatorBD>> PostEnergyIndicator(EnergyIndicatorBD item)
        {
            _context.EnergyIndicators.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEnergyIndicator), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnergyIndicator(int id, EnergyIndicatorBD item)
        {
            if (id != item.Id) return BadRequest();

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnergyIndicator(int id)
        {
            var item = await _context.EnergyIndicators.FindAsync(id);
            if (item == null) return NotFound();

            _context.EnergyIndicators.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
