using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T5.PR1.Practica_1.Data;
using T5.PR1.Practica_1.Model;

namespace T4.PR1.Practica_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaterConsumptionsController : ControllerBase
    {
        private readonly EcoEnergyDbContext _context;

        public WaterConsumptionsController(EcoEnergyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WaterConsumptionBD>>> GetWaterConsumptions()
        {
            return await _context.WaterConsumptions.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WaterConsumptionBD>> GetWaterConsumption(int id)
        {
            var item = await _context.WaterConsumptions.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<WaterConsumptionBD>> PostWaterConsumption(WaterConsumptionBD item)
        {
            _context.WaterConsumptions.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetWaterConsumption), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutWaterConsumption(int id, WaterConsumptionBD item)
        {
            if (id != item.Id) return BadRequest();

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWaterConsumption(int id)
        {
            var item = await _context.WaterConsumptions.FindAsync(id);
            if (item == null) return NotFound();

            _context.WaterConsumptions.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
