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

        // GET: api/APIApplicationUsers
        //returns First Name, Last Name, email, and city of ApplicationUser
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
                return BadRequest(new { Error = "1" });
            }
            
            /*
            if (!await _userManager.CheckPasswordAsync(applicationUser, Password))
            {
                return BadRequest(new { Error = "2" });
            }
            */
            
            var myEvent = await _context.Events.SingleOrDefaultAsync(m => m.Evid == Convert.ToInt64(Evid));
            
            if (myEvent == null)
            {
                return BadRequest(new { Error = "3" });
            }

            var UserEvent = await _context.UserEvents.SingleOrDefaultAsync(ue => ue.Userid == applicationUser.Id && ue.Evid == myEvent.Evid);

            if (UserEvent == null)
            {
                return Ok(new { Exists = false, Attending = false });
            }

            return Ok(new { Exists = true, Attending = UserEvent.Attending });
        }


    }
}
