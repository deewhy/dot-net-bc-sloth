using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotNetBcBackend.Data;
using DotNetBcBackend.Models;
using Microsoft.AspNetCore.Authorization;

namespace DotNetBcBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/APISponsors")]
    public class APISponsorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public APISponsorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/APISponsors
        [HttpGet]
        public IEnumerable<Sponsor> GetSponsors()
        {
            return _context.Sponsors;
        }

        // GET: api/APISponsors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSponsor([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sponsor = await _context.Sponsors.SingleOrDefaultAsync(m => m.Sponid == id);

            if (sponsor == null)
            {
                return NotFound();
            }

            return Ok(sponsor);
        }

        /*
        // PUT: api/APISponsors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSponsor([FromRoute] long id, [FromBody] Sponsor sponsor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sponsor.Sponid)
            {
                return BadRequest();
            }

            _context.Entry(sponsor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SponsorExists(id))
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

        // POST: api/APISponsors
        [HttpPost]
        public async Task<IActionResult> PostSponsor([FromBody] Sponsor sponsor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Sponsors.Add(sponsor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSponsor", new { id = sponsor.Sponid }, sponsor);
        }

        // DELETE: api/APISponsors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSponsor([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sponsor = await _context.Sponsors.SingleOrDefaultAsync(m => m.Sponid == id);
            if (sponsor == null)
            {
                return NotFound();
            }

            _context.Sponsors.Remove(sponsor);
            await _context.SaveChangesAsync();

            return Ok(sponsor);
        }
        */

        private bool SponsorExists(long id)
        {
            return _context.Sponsors.Any(e => e.Sponid == id);
        }
    }
}