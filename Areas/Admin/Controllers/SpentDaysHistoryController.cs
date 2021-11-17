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
    public class SpentDaysHistoryController : Controller
    {
        private readonly FurloughContext _context;

        public SpentDaysHistoryController(FurloughContext context)
        {
            _context = context;
        }

        // GET: Admin/SpentDaysHistory
        public async Task<IActionResult> Index()
        {
            var furloughContext = _context.SpentDaysHistories.Include(s => s.RequestHistory);
            return View(await furloughContext.ToListAsync());
        }

        // GET: Admin/SpentDaysHistory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spentDaysHistory = await _context.SpentDaysHistories
                .Include(s => s.RequestHistory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spentDaysHistory == null)
            {
                return NotFound();
            }

            return View(spentDaysHistory);
        }

        // GET: Admin/SpentDaysHistory/Create
        public IActionResult Create()
        {
            ViewData["RequestHistoryId"] = new SelectList(_context.RequestHistories, "Id", "Id");
            return View();
        }

        // POST: Admin/SpentDaysHistory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RequestHistoryId,BaseDays,BonusDays,PrevYearDays")] SpentDaysHistory spentDaysHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(spentDaysHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RequestHistoryId"] = new SelectList(_context.RequestHistories, "Id", "Id", spentDaysHistory.RequestHistoryId);
            return View(spentDaysHistory);
        }

        // GET: Admin/SpentDaysHistory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spentDaysHistory = await _context.SpentDaysHistories.FindAsync(id);
            if (spentDaysHistory == null)
            {
                return NotFound();
            }
            ViewData["RequestHistoryId"] = new SelectList(_context.RequestHistories, "Id", "Id", spentDaysHistory.RequestHistoryId);
            return View(spentDaysHistory);
        }

        // POST: Admin/SpentDaysHistory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RequestHistoryId,BaseDays,BonusDays,PrevYearDays")] SpentDaysHistory spentDaysHistory)
        {
            if (id != spentDaysHistory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(spentDaysHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpentDaysHistoryExists(spentDaysHistory.Id))
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
            ViewData["RequestHistoryId"] = new SelectList(_context.RequestHistories, "Id", "Id", spentDaysHistory.RequestHistoryId);
            return View(spentDaysHistory);
        }

        // GET: Admin/SpentDaysHistory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spentDaysHistory = await _context.SpentDaysHistories
                .Include(s => s.RequestHistory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spentDaysHistory == null)
            {
                return NotFound();
            }

            return View(spentDaysHistory);
        }

        // POST: Admin/SpentDaysHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var spentDaysHistory = await _context.SpentDaysHistories.FindAsync(id);
            _context.SpentDaysHistories.Remove(spentDaysHistory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpentDaysHistoryExists(int id)
        {
            return _context.SpentDaysHistories.Any(e => e.Id == id);
        }
    }
}
