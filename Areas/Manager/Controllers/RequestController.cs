using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Furlough.DAL;
using Furlough.DAL.Models;
using System.Globalization;

namespace Furlough.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class RequestController : Controller
    {
        private readonly FurloughContext _context;
        private readonly DAL.Request _contextRequest;

        public RequestController(FurloughContext context, DAL.Request contextRequest)
        {
            _context = context;
            _contextRequest = contextRequest;
        }

        // GET: Manager/Request
        public async Task<IActionResult> Index()
        {
            var departmentId = 1; //Get DepartmentId by HttpContext

            return View(_contextRequest.GetByDepartment(departmentId));
        }

        // GET: Manager/Request/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Requests
                .Include(r => r.RequestStatus)
                .Include(r => r.RequestType)
                .Include(r => r.RequestedByNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // GET: Manager/Request/Create
        public IActionResult Create()
        {
            ViewData["RequestStatusId"] = new SelectList(_context.RequestStatuses, "Id", "Id");
            ViewData["RequestTypeId"] = new SelectList(_context.RequestTypes, "Id", "Id");
            ViewData["RequestedByUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Manager/Request/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateFrom,DateUntil,RequestedByUserId,RequestedOn,RequestStatusId,PaidDays,RequestTypeId")] DAL.Models.Request request)
        {
            if (ModelState.IsValid)
            {
                _context.Add(request);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RequestStatusId"] = new SelectList(_context.RequestStatuses, "Id", "Id", request.RequestStatusId);
            ViewData["RequestTypeId"] = new SelectList(_context.RequestTypes, "Id", "Id", request.RequestTypeId);
            ViewData["RequestedByUserId"] = new SelectList(_context.Users, "Id", "Id", request.RequestedByUserId);
            return View(request);
        }

        // GET: Manager/Request/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            ViewData["RequestStatusId"] = new SelectList(_context.RequestStatuses, "Id", "Id", request.RequestStatusId);
            ViewData["RequestTypeId"] = new SelectList(_context.RequestTypes, "Id", "Id", request.RequestTypeId);
            ViewData["RequestedByUserId"] = new SelectList(_context.Users, "Id", "Id", request.RequestedByUserId);
            return View(request);
        }

        // POST: Manager/Request/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateFrom,DateUntil,RequestedByUserId,RequestedOn,RequestStatusId,PaidDays,RequestTypeId")] DAL.Models.Request request)
        {
            if (id != request.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(request);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestExists(request.Id))
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
            ViewData["RequestStatusId"] = new SelectList(_context.RequestStatuses, "Id", "Id", request.RequestStatusId);
            ViewData["RequestTypeId"] = new SelectList(_context.RequestTypes, "Id", "Id", request.RequestTypeId);
            ViewData["RequestedByUserId"] = new SelectList(_context.Users, "Id", "Id", request.RequestedByUserId);
            return View(request);
        }

        // GET: Manager/Request/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Requests
                .Include(r => r.RequestStatus)
                .Include(r => r.RequestType)
                .Include(r => r.RequestedByNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        [AllowAnonymous]
        [HttpPost]

        public IActionResult ChangeLang(string lang, string returnUrl)
        {
            var cultureInfo = new CultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);

            this.HttpContext.Response.Cookies.Append
            (
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(lang))
            );

            return LocalRedirect(returnUrl);
        }

        // POST: Manager/Request/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestExists(int id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }
    }
}
