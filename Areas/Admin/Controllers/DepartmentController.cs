using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Furlough.DAL;
using Furlough.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Furlough.Models.Mapper;

namespace Furlough.Areas.Admin.Controllers
{
    [AllowAnonymous]
    [Area("Admin")]
    public class DepartmentController : Controller
    {
        private readonly FurloughContext _context;
        private readonly DAL.Department _contextDepartment;
        private readonly DalMapper _dalMapper;
        private ViewModelMapper _vmMapper;

        public DepartmentController(FurloughContext context, DAL.Department contextDepartment,
            Models.Mapper.DalMapper dalMapper, Models.Mapper.ViewModelMapper vmMapper)
        {
            //Context variables
            _context = context;
            _contextDepartment = contextDepartment;

            //Object Mappers
            _dalMapper = dalMapper;
            _vmMapper = vmMapper;
        }

        // GET: Admin/Department
        public async Task<IActionResult> Index()
        {
            return View(_contextDepartment.GetAll());
        }

        // GET: Admin/Department/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var department = await _context.Departments
            //    .FirstOrDefaultAsync(m => m.Id == id);

            var department = _contextDepartment.GetById(id.Value);
            if (department == null)
            {
                return NotFound();
            }

            return View(_vmMapper.DepartmentMap(department));
        }

        // GET: Admin/Department/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Department/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //_context.Add(department);
                    //await _context.SaveChangesAsync();
                    var result = _contextDepartment.Add(_dalMapper.DalDepartmentMap(department));
                    return RedirectToAction(nameof(Index));
                }

                return View(department);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error in DepartmentController of Admin Area in Create [POST] " + e.Message);
                Console.ResetColor();
                return RedirectToAction(nameof(Index));
            }
            
        }

        // GET: Admin/Department/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var department = _contextDepartment.GetById(id.Value);

                if (department == null)
                {
                    return NotFound();
                }
                return View(_vmMapper.DepartmentMap(department));
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();

                return NotFound();
            }
            
        }

        // POST: Admin/Department/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Models.Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(department);
                    //await _context.SaveChangesAsync();
                    var result = _contextDepartment.Edit(_dalMapper.DalDepartmentMap(department));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Id))
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
            return View(department);
        }

        // GET: Admin/Department/Delete/5
        public async Task<IActionResult> Delete(int id, string message = null)
        {
            var department = _contextDepartment.GetById(id);
            if (department == null)
            {
                return NotFound();
            }

            ViewBag.FkError = message != null && message.Contains("FK__EMPLOYEE__Depart__4222D4EF");

            return View(_vmMapper.DepartmentMap(department));
        }

        // POST: Admin/Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var department = _contextDepartment.GetById(id);
                var result = _contextDepartment.Delete(department.Id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                var message = e.Message;
                return RedirectToAction(nameof(Delete), new { message });
                throw;
            }
        }

        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.Id == id);
        }
    }
}
