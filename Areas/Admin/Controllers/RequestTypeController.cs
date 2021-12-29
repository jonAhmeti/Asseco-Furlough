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
    public class RequestTypeController : Controller
    {
        private readonly DAL.RequestType _contextRequestType;
        private readonly DalMapper _dalMapper;

        public RequestTypeController(DAL.RequestType contextRequestType,
            DalMapper dalMapper)
        {
            _contextRequestType = contextRequestType;
            _dalMapper = dalMapper;
        }

        // GET: Admin/RequestType
        public async Task<IActionResult> Index()
        {
            return View(_contextRequestType.GetAll());
        }

        // GET: Admin/RequestType/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var requestType = _contextRequestType.GetById(id);
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
                var result = _contextRequestType.Add(_dalMapper.DalRequestTypeMap(requestType));
                return RedirectToAction(nameof(Index));
            }
            return View(requestType);
        }

        // GET: Admin/RequestType/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var requestType = _contextRequestType.GetById(id);
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
                    var result = _contextRequestType.Edit(_dalMapper.DalRequestTypeMap(requestType));
                }
                catch (Exception e)
                {
                   Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ResetColor();
                    return BadRequest(e.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(requestType);
        }

        // GET: Admin/RequestType/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var requestType = _contextRequestType.GetById(id);
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
            var requestType = _contextRequestType.GetById(id);
            var result = _contextRequestType.Delete(requestType.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
