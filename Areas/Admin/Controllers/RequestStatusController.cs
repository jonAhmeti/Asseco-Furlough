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
    public class RequestStatusController : Controller
    {
        private readonly FurloughContext _context;

        public RequestStatusController(FurloughContext context)
        {
            _context = context;
        }

        // GET: Admin/RequestStatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.RequestStatuses.ToListAsync());
        }

        // GET: Admin/RequestStatus/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestStatus = await _context.RequestStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requestStatus == null)
            {
                return NotFound();
            }

            return View(requestStatus);
        }

        // GET: Admin/RequestStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/RequestStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type")] RequestStatus requestStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(requestStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(requestStatus);
        }

        // GET: Admin/RequestStatus/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestStatus = await _context.RequestStatuses.FindAsync(id);
            if (requestStatus == null)
            {
                return NotFound();
            }
            return View(requestStatus);
        }

        // POST: Admin/RequestStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("Id,Type")] RequestStatus requestStatus)
        {
            if (id != requestStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requestStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestStatusExists(requestStatus.Id))
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
            return View(requestStatus);
        }

        // GET: Admin/RequestStatus/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestStatus = await _context.RequestStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requestStatus == null)
            {
                return NotFound();
            }

            return View(requestStatus);
        }

        // POST: Admin/RequestStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            var requestStatus = await _context.RequestStatuses.FindAsync(id);
            _context.RequestStatuses.Remove(requestStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestStatusExists(byte id)
        {
            return _context.RequestStatuses.Any(e => e.Id == id);
        }
    }
}
