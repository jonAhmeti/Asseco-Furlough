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
    public class RoleController : Controller
    {
        private DAL.Role _contextRole;
        private DalMapper _dalMapper;
        private ViewModelMapper _vmMapper;

        public RoleController(DAL.Role contextRole, 
            Models.Mapper.DalMapper dalMapper, Models.Mapper.ViewModelMapper vmMapper)
        {
            _contextRole = contextRole;

            _dalMapper = dalMapper;
            _vmMapper = vmMapper;
        }

        // GET: Admin/Role
        public async Task<IActionResult> Index()
        {
            var roles = new List<Models.Role>();
            foreach (var item in _contextRole.GetAll())
            {
                roles.Add(_vmMapper.RoleMap(item));
            }

            return View(roles);
        }

        // GET: Admin/Role/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = _contextRole.GetById(id.Value);
            if (role == null)
            {
                return NotFound();
            }

            return View(_vmMapper.RoleMap(role));
        }

        // GET: Admin/Role/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Role/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.Role role)
        {
            if (ModelState.IsValid)
            {
                _contextRole.Add(_dalMapper.DalRoleMap(role));
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: Admin/Role/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = _contextRole.GetById(id.Value);
            if (role == null)
            {
                return NotFound();
            }
            return View(_vmMapper.RoleMap(role));
        }

        // POST: Admin/Role/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Models.Role role)
        {
            if (id != role.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = _contextRole.Edit(_dalMapper.DalRoleMap(role));
                }
                catch (DbUpdateConcurrencyException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ResetColor();

                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: Admin/Role/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = _contextRole.GetById(id.Value);
            if (role == null)
            {
                return NotFound();
            }

            return View(_vmMapper.RoleMap(role));
        }

        // POST: Admin/Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var role = _contextRole.GetById(id);
            var result = _contextRole.Delete(role.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
