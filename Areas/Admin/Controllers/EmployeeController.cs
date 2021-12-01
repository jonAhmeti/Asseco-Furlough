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
    public class EmployeeController : Controller
    {
        private readonly FurloughContext _context;
        private readonly DAL.Employee _contextEmployee;
        private readonly ViewModelMapper _vmMapper;
        private readonly DalMapper _dalMapper;

        public EmployeeController(FurloughContext context, DAL.Employee contextEmployee,
            Models.Mapper.ViewModelMapper vmMapper, Models.Mapper.DalMapper dalMapper)
        {
            _context = context;
            _contextEmployee = contextEmployee;
            _vmMapper = vmMapper;
            _dalMapper = dalMapper;
        }

        // GET: Admin/Employee
        public async Task<IActionResult> Index()
        {
            var furloughContext = _context.Employees.Include(e => e.Department).Include(e => e.Position).Include(e => e.User);
            return View(await furloughContext.ToListAsync());
        }

        // GET: Admin/Employee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Position)
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Admin/Employee/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id");
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Admin/Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.Employee employee)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(employee);
                _contextEmployee.Add(_dalMapper.DalEmployeeMap(employee));
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", employee.DepartmentId);
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Id", employee.PositionId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", employee.UserId);
            return View(employee);
        }

        // GET: Admin/Employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var employee = _contextEmployee.GetById(id.Value);
                if (employee == null)
                {
                    return NotFound();
                }

                ViewData["Departments"] = new SelectList(_context.Departments, "Id", "Name", employee.DepartmentId);
                ViewData["Positions"] = new SelectList(_context.Positions, "Id", "Title", employee.PositionId);
                ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", employee.UserId);
                return View(_vmMapper.EmployeeMap(employee));
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error in EmployeeController of Admin Area onEdit [GET] " + e.Message);
                Console.ResetColor();

                return RedirectToAction("Index");
            }
        }

        // POST: Admin/Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Models.Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(employee);
                    //await _context.SaveChangesAsync();
                    var result = _contextEmployee.Edit(_dalMapper.DalEmployeeMap(employee));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
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
            ViewData["Departments"] = new SelectList(_context.Departments, "Id", "Name", employee.DepartmentId);
            ViewData["Positions"] = new SelectList(_context.Positions, "Id", "Title", employee.PositionId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", employee.UserId);
            return View(employee);
        }

        // GET: Admin/Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Position)
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Admin/Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
