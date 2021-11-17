using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Furlough.DAL;
using Furlough.DAL.Models;

namespace Furlough.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RequestHistoryController : Controller
    {
        private readonly FurloughContext _context;

        public RequestHistoryController(FurloughContext context)
        {
            _context = context;
        }

        // GET: Admin/RequestHistory
        public async Task<IActionResult> Index()
        {
            var furloughContext = _context.RequestHistories.Include(r => r.AlteredByNavigation).Include(r => r.AlteredToNavigation).Include(r => r.Request);
            return View(await furloughContext.ToListAsync());
        }

        // GET: Admin/RequestHistory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestHistory = await _context.RequestHistories
                .Include(r => r.AlteredByNavigation)
                .Include(r => r.AlteredToNavigation)
                .Include(r => r.Request)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requestHistory == null)
            {
                return NotFound();
            }

            return View(requestHistory);
        }

        // GET: Admin/RequestHistory/Create
        public IActionResult Create()
        {
            ViewData["AlteredBy"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["AlteredTo"] = new SelectList(_context.RequestStatuses, "Id", "Id");
            ViewData["RequestId"] = new SelectList(_context.Requests, "Id", "Id");
            return View();
        }

        // POST: Admin/RequestHistory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RequestId,AlteredBy,AlteredOn,AlteredTo")] RequestHistory requestHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(requestHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlteredBy"] = new SelectList(_context.Users, "Id", "Id", requestHistory.AlteredBy);
            ViewData["AlteredTo"] = new SelectList(_context.RequestStatuses, "Id", "Id", requestHistory.AlteredTo);
            ViewData["RequestId"] = new SelectList(_context.Requests, "Id", "Id", requestHistory.RequestId);
            return View(requestHistory);
        }

        // GET: Admin/RequestHistory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestHistory = await _context.RequestHistories.FindAsync(id);
            if (requestHistory == null)
            {
                return NotFound();
            }
            ViewData["AlteredBy"] = new SelectList(_context.Users, "Id", "Id", requestHistory.AlteredBy);
            ViewData["AlteredTo"] = new SelectList(_context.RequestStatuses, "Id", "Id", requestHistory.AlteredTo);
            ViewData["RequestId"] = new SelectList(_context.Requests, "Id", "Id", requestHistory.RequestId);
            return View(requestHistory);
        }

        // POST: Admin/RequestHistory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RequestId,AlteredBy,AlteredOn,AlteredTo")] RequestHistory requestHistory)
        {
            if (id != requestHistory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requestHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestHistoryExists(requestHistory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlteredBy"] = new SelectList(_context.Users, "Id", "Id", requestHistory.AlteredBy);
            ViewData["AlteredTo"] = new SelectList(_context.RequestStatuses, "Id", "Id", requestHistory.AlteredTo);
            ViewData["RequestId"] = new SelectList(_context.Requests, "Id", "Id", requestHistory.RequestId);
            return View(requestHistory);
        }

        // GET: Admin/RequestHistory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestHistory = await _context.RequestHistories
                .Include(r => r.AlteredByNavigation)
                .Include(r => r.AlteredToNavigation)
                .Include(r => r.Request)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requestHistory == null)
            {
                return NotFound();
            }

            return View(requestHistory);
        }

        // POST: Admin/RequestHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var requestHistory = await _context.RequestHistories.FindAsync(id);
            _context.RequestHistories.Remove(requestHistory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestHistoryExists(int id)
        {
            return _context.RequestHistories.Any(e => e.Id == id);
        }
    }
}
