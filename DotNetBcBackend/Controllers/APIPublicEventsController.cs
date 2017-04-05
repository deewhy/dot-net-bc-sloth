using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotNetBcBackend.Data;
using DotNetBcBackend.Models;

namespace DotNetBcBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/APIPublicEvents")]
    public class APIPublicEventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public APIPublicEventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/APIPublicEvents
        [HttpGet]
        public IEnumerable<Event> GetEvents()
        {
            var eventsList = _context.Events.OrderBy(ev => ev.Evdate).ToList();
            return eventsList.Skip(eventsList.Count - 10).Take(10);
        }
    }
}