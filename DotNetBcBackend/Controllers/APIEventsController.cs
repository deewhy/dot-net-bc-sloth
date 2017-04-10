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
using System.Diagnostics;

namespace DotNetBcBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/APIEvents")]
    [Authorize]
    public class APIEventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public APIEventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/APIEvents
        [HttpGet]
        public IEnumerable<Event> GetEvents()
        {
            return _context.Events.OrderBy(ev => ev.Evdate);
        }

        // GET: api/APIEvents/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var @event = await _context.Events.SingleOrDefaultAsync(m => m.Evid == id);

            if (@event == null)
            {
                return NotFound();
            }

            return Ok(@event);
        }
        
        private bool EventExists(long id)
        {
            return _context.Events.Any(e => e.Evid == id);
        }
    }
}