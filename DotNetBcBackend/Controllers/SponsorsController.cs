using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DotNetBcBackend.Data;
using DotNetBcBackend.Models;

namespace DotNetBcBackend.Controllers
{
    public class SponsorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SponsorsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Sponsors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sponsors.ToListAsync());
        }

        // GET: Sponsors/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sponsor = await _context.Sponsors
                .SingleOrDefaultAsync(m => m.Sponid == id);
            if (sponsor == null)
            {
                return NotFound();
            }

            return View(sponsor);
        }

        // GET: Sponsors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sponsors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Sponid,Sponname,Sponimg,Sponlink")] Sponsor sponsor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sponsor);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sponsor);
        }

        // GET: Sponsors/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sponsor = await _context.Sponsors.SingleOrDefaultAsync(m => m.Sponid == id);
            if (sponsor == null)
            {
                return NotFound();
            }
            return View(sponsor);
        }

        // POST: Sponsors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Sponid,Sponname,Sponimg,Sponlink")] Sponsor sponsor)
        {
            if (id != sponsor.Sponid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sponsor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SponsorExists(sponsor.Sponid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(sponsor);
        }

        // GET: Sponsors/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sponsor = await _context.Sponsors
                .SingleOrDefaultAsync(m => m.Sponid == id);
            if (sponsor == null)
            {
                return NotFound();
            }

            return View(sponsor);
        }

        // POST: Sponsors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var sponsor = await _context.Sponsors.SingleOrDefaultAsync(m => m.Sponid == id);
            _context.Sponsors.Remove(sponsor);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SponsorExists(long id)
        {
            return _context.Sponsors.Any(e => e.Sponid == id);
        }
    }
}
