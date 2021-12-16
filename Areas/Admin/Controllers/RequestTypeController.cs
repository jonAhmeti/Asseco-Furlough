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
    public class RequestTypeController : Controller
    {
        private readonly FurloughContext _context;

        public RequestTypeController(FurloughContext context)
        {
            _context = context;
        }

        // GET: Admin/RequestType
        public async Task<IActionResult> Index()
        {
            return View(await _context.RequestTypes.ToListAsync());
        }

        // GET: Admin/RequestType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestType = await _context.RequestTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requestType == null)
            {
                return NotFound();
            }

            return View(requestType);
        }

        // GET: Admin/RequestType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/RequestType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type")] Models.RequestType requestType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(requestType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(requestType);
        }

        // GET: Admin/RequestType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestType = await _context.RequestTypes.FindAsync(id);
            if (requestType == null)
            {
                return NotFound();
            }
            return View(requestType);
        }

        // POST: Admin/RequestType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type")] Models.RequestType requestType)
        {
            if (id != requestType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requestType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestTypeExists(requestType.Id))
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
            return View(requestType);
        }

        // GET: Admin/RequestType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestType = await _context.RequestTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requestType == null)
            {
                return NotFound();
            }

            return View(requestType);
        }

        // POST: Admin/RequestType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var requestType = await _context.RequestTypes.FindAsync(id);
            _context.RequestTypes.Remove(requestType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestTypeExists(int id)
        {
            return _context.RequestTypes.Any(e => e.Id == id);
        }
    }
}
