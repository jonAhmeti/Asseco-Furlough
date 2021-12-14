using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Furlough.DAL;
using Furlough.DAL.Models;
using Furlough.Models.Mapper;

namespace Furlough.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RequestController : Controller
    {
        private readonly FurloughContext _context;
        private readonly DAL.Request _contextRequest;
        private readonly ViewModelMapper _vmMapper;

        public RequestController(FurloughContext context, DAL.Request contextRequest, ViewModelMapper vmMapper)
        {
            _context = context;
            _contextRequest = contextRequest;

            _vmMapper = vmMapper;
        }

        // GET: Admin/Request
        public async Task<IActionResult> Index()
        {
            var userDepartment = "";
            try
            {
                userDepartment = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Department").Value;
                var departmentRequests = _contextRequest.GetByDepartment(int.Parse(userDepartment));
                return View(departmentRequests);
            }
            catch (Exception e)
            {
                if (userDepartment == "")
                    return RedirectToAction("Index", "Home", new { Area = "" });
                  //return BadRequest("Not logged in, or this user doesn't belong to a department.");

                Console.WriteLine(e.Message);
            }
            return RedirectToAction("Index", "Home", new { Area = "" });
        }

        // GET: Admin/Request/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var request = _contextRequest.GetById(id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // GET: Admin/Request/Create
        public IActionResult Create()
        {
            ViewData["RequestStatusId"] = new SelectList(_context.RequestStatuses, "Id", "Id");
            ViewData["RequestTypeId"] = new SelectList(_context.RequestTypes, "Id", "Id");
            ViewData["RequestedByUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Admin/Request/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateFrom,DateUntil,RequestedByUserId,RequestedOn,RequestStatusId,PaidDays,RequestTypeId")] Models.Request request)
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

        // GET: Admin/Request/Edit/5
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

        // POST: Admin/Request/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateFrom,DateUntil,RequestedByUserId,RequestedOn,RequestStatusId,PaidDays,RequestTypeId")] Models.Request request)
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

        // GET: Admin/Request/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var request = _contextRequest.GetById(id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // POST: Admin/Request/Delete/5
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
