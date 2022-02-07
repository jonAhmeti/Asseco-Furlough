using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Furlough.DAL;
using Furlough.Models.Mapper;

namespace Furlough.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PositionController : Controller
    {
        private readonly FurloughContext _context;
        private readonly DAL.Position _contextPosition;
        private readonly DalMapper _dalMapper;
        private readonly ViewModelMapper _vmMapper;

        public PositionController(FurloughContext context, DAL.Position contextPosition,
            Models.Mapper.DalMapper dalMapper, Models.Mapper.ViewModelMapper vmMapper)
        {
            _context = context;
            _contextPosition = contextPosition;

            _dalMapper = dalMapper;
            _vmMapper = vmMapper;
        }

        // GET: Admin/Position
        public async Task<IActionResult> Index()
        {
            var positions = new List<Models.Position>();
            foreach (var item in _contextPosition.GetAll())
            {
                positions.Add(_vmMapper.PositionMap(item));
            }
            return View(positions);
        }

        // GET: Admin/Position/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var position = _contextPosition.GetById(id.Value);
            if (position == null)
            {
                return NotFound();
            }

            return View(_vmMapper.PositionMap(position));
        }

        // GET: Admin/Position/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Position/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.Position position)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(position);
                //await _context.SaveChangesAsync();
                var result = _contextPosition.Add(position.Title);

                return RedirectToAction(nameof(Index));
            }
            return View(position);
        }

        // GET: Admin/Position/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var position = _contextPosition.GetById(id.Value);
            if (position == null)
            {
                return NotFound();
            }
            return View(_vmMapper.PositionMap(position));
        }

        // POST: Admin/Position/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Models.Position position)
        {
            if (id != position.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = _contextPosition.Edit(_dalMapper.DalPositionMap(position));
                    //_context.Update(position);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_contextPosition.GetById(id) == null)
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
            return View(position);
        }

        // GET: Admin/Position/Delete/5
        public async Task<IActionResult> Delete(int id, string? message = null)
        {
            var position = _contextPosition.GetById(id);
            if (position == null)
            {
                return NotFound();
            }

            ViewData["Message"] = message;

            return View(_vmMapper.PositionMap(position));
        }

        // POST: Admin/Position/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var position = _contextPosition.GetById(id);
                var result = _contextPosition.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                string? message = "";
                if (e.Message.Contains("FK__Departmen__Posit__"))
                    message = "This position is already being used in a department";

                return RedirectToAction(nameof(Delete), new { message });
            }
        }
    }
}
