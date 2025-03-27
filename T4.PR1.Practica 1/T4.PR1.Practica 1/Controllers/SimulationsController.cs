using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T5.PR1.Practica_1.Data;
using T5.PR1.Practica_1.Model;

namespace T4.PR1.Practica_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimulationsController : ControllerBase
    {
        private readonly EcoEnergyDbContext _context;

        public SimulationsController(EcoEnergyDbContext context)
        {
            _context = context;
        }

        // GET: api/Simulations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SimulationBD>>> GetSimulations()
        {
            return await _context.Simulations.ToListAsync();
        }

        // GET: api/Simulations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SimulationBD>> GetSimulation(int id)
        {
            var simulation = await _context.Simulations.FindAsync(id);
            if (simulation == null)
            {
                return NotFound();
            }
            return simulation;
        }

        // POST: api/Simulations
        [HttpPost]
        public async Task<ActionResult<SimulationBD>> PostSimulation(SimulationBD simulation)
        {
            _context.Simulations.Add(simulation);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSimulation), new { id = simulation.Id }, simulation);
        }

        // PUT: api/Simulations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSimulation(int id, SimulationBD simulation)
        {
            if (id != simulation.Id)
            {
                return BadRequest();
            }

            _context.Entry(simulation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Simulations.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Simulations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSimulation(int id)
        {
            var simulation = await _context.Simulations.FindAsync(id);
            if (simulation == null)
            {
                return NotFound();
            }

            _context.Simulations.Remove(simulation);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
