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
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DotNetBcBackend.Models.AccountViewModels;
using Microsoft.Extensions.Logging;

namespace DotNetBcBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/APIAttendEvents")]
    public class APIAttendEvents : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public APIAttendEvents(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/APIAttendEvents
        [HttpGet]
        public async Task<IActionResult> GetExistsAttending([FromHeader] string UserName, [FromHeader] string Password, [FromHeader] string Evid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicationUser = await _context.ApplicationUsers.SingleOrDefaultAsync(m => m.UserName == UserName);

            if (applicationUser == null)
            {
                return BadRequest();
            }
            
            if (!await _userManager.CheckPasswordAsync(applicationUser, Password))
            {
                return BadRequest();
            }
            
            var myEvent = await _context.Events.SingleOrDefaultAsync(m => m.Evid == Convert.ToInt64(Evid));
            
            if (myEvent == null)
            {
                return BadRequest();
            }

            var UserEvent = await _context.UserEvents.SingleOrDefaultAsync(ue => ue.Userid == applicationUser.Id && ue.Evid == Convert.ToInt64(Evid));

            if (UserEvent == null)
            {
                return Ok(new { Exists = false, Attending = false });
            }

            if (UserEvent.Attending == true)
            {
                return Ok(new { Exists = true, Attending = true });
            } else
            {
                return Ok(new { Exists = true, Attending = false });
            }
        }

        // POST: api/APIAttendEvents
        [HttpPost]
        public async Task<IActionResult> PostUserEvent([FromBody] APIAttendEventsModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model == null)
            {
                return BadRequest(new { Error = "JSON object was found to be null." });
            }

            var applicationUser = await _context.ApplicationUsers.SingleOrDefaultAsync(m => m.UserName == model.UserName);

            if (applicationUser == null)
            {
                return BadRequest();
            }

            if (!await _userManager.CheckPasswordAsync(applicationUser, model.Password))
            {
                return BadRequest();
            }

            var newUserEvent = new UserEvents
            {
                Evid = model.Evid,
                Userid = applicationUser.Id,
                Attending = true
            };

            _context.Entry<UserEvents>(newUserEvent).State = EntityState.Added;

            try
            {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/AttendEvents
        [HttpPut]
        public async Task<IActionResult> PutUserEvent([FromBody] APIAttendEventsModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model == null)
            {
                return BadRequest(new { Error = "JSON object was found to be null." });
            }

            var applicationUser = await _context.ApplicationUsers.SingleOrDefaultAsync(m => m.UserName == model.UserName);

            if (applicationUser == null)
            {
                return BadRequest();
            }

            if (!await _userManager.CheckPasswordAsync(applicationUser, model.Password))
            {
                return BadRequest();
            }

            var myEvent = await _context.Events.SingleOrDefaultAsync(m => m.Evid == model.Evid);

            if (myEvent == null)
            {
                return BadRequest();
            }

            var UserEvent = await _context.UserEvents.SingleOrDefaultAsync(ue => ue.Userid == applicationUser.Id && ue.Evid == myEvent.Evid);

            if (UserEvent == null)
            {
                return BadRequest();
            }

            UserEvent.Attending = model.Attending;

            _context.Entry(UserEvent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
