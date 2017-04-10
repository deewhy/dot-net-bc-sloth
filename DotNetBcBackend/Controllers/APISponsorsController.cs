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
        
        private bool SponsorExists(long id)
        {
            return _context.Sponsors.Any(e => e.Sponid == id);
        }
    }
}