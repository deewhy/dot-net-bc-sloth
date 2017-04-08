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

namespace DotNetBcBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/APIApplicationUsers")]
    [Authorize]
    public class APIApplicationUsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public APIApplicationUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/APIApplicationUsers
        [HttpGet]
        public IEnumerable<ApplicationUser> GetApplicationUsers()
        {
            return _context.ApplicationUsers;
            
        }

        // GET: api/APIApplicationUsers/userName
        [HttpGet("{username}")]
        public async Task<IActionResult> GetApplicationUserByName([FromRoute] string userName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicationUser = await _context.ApplicationUsers.SingleOrDefaultAsync(m => m.UserName == userName);
            ApplicationUser user = new ApplicationUser();
            user.FirstName = applicationUser.FirstName;
            user.LastName = applicationUser.LastName;
            user.Email = applicationUser.Email;
            user.City = applicationUser.City;
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        //// GET: api/APIApplicationUsers/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetApplicationUser([FromRoute] string id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var applicationUser = await _context.ApplicationUsers.SingleOrDefaultAsync(m => m.Id == id);

        //    if (applicationUser == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(applicationUser);
        //}

        // PUT: api/APIApplicationUsers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicationUser([FromRoute] string id, [FromBody] ApplicationUser applicationUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != applicationUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(applicationUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationUserExists(id))
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

        // POST: api/APIApplicationUsers
        [HttpPost]
        public async Task<IActionResult> PostApplicationUser([FromBody] ApplicationUser applicationUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ApplicationUsers.Add(applicationUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplicationUser", new { id = applicationUser.Id }, applicationUser);
        }

        // DELETE: api/APIApplicationUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicationUser = await _context.ApplicationUsers.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            _context.ApplicationUsers.Remove(applicationUser);
            await _context.SaveChangesAsync();

            return Ok(applicationUser);
        }

        private bool ApplicationUserExists(string id)
        {
            return _context.ApplicationUsers.Any(e => e.Id == id);
        }
    }
}