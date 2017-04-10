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
    [Route("api/APIApplicationUsers")]
    public class APIApplicationUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        
        public APIApplicationUsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            
        }

        // GET: api/APIApplicationUsers/myprofile
        //returns First Name, Last Name, email, and city of ApplicationUser
        [HttpGet("myprofile")]
        public async Task<IActionResult> GetApplicationUserByName([FromHeader] string UserName, [FromHeader] string Password)
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

            var user = new { FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName,
                Email =applicationUser.Email,
                City =applicationUser.City,
                NotifyJobs =applicationUser.NotifyJobs };
            return Ok(user);
        }

        /*
        // GET: api/APIApplicationUsers/5
        //returns First Name, Last Name , email , and city of User
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicationUserById([FromRoute] string id)
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

            var user = new { FirstName = applicationUser.FirstName, LastName = applicationUser.LastName, Email = applicationUser.Email, City = applicationUser.City };
            return Ok(user);

            
        }
        */

        // PUT: api/APIApplicationUsers/myprofile
        [HttpPut("myprofile")]
        public async Task<IActionResult> PutApplicationUser([FromBody] APIUpdateProfileModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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

            applicationUser.Email = model.Email;
            applicationUser.FirstName = model.FirstName;
            applicationUser.LastName = model.LastName;
            applicationUser.City = model.City;
            applicationUser.NotifyJobs = model.NotifyJobs;

            _context.Entry(applicationUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationUserExists(model.UserName))
                {
                    return BadRequest();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/APIApplicationUsers
        // For registering new users.
        [HttpPost]
        public async Task<IActionResult> PostApplicationUser([FromBody] APIRegisterModel model)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (model == null)
            {
                return BadRequest(new { Error = "JSON object was found to be null." });
            }

            var newUser = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                City = model.City,
                NotifyJobs = model.NotifyJobs,
                LockoutEnabled = true,
                IsActive = true,
                Created = DateTime.Now
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(newUser, isPersistent: false);

                //give newly registered member the role of Member
                await _userManager.AddToRoleAsync(newUser, "Member");

                return CreatedAtAction("GetApplicationUser", new { UserName = model.UserName });
            }
            
            return BadRequest(new { Error = "User could not be created" });
        }

        /*
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
        */

        private bool ApplicationUserExists(string userName)
        {
            return _context.ApplicationUsers.Any(e => e.UserName == userName);
        }
    }
}