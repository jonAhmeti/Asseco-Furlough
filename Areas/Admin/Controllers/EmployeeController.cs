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
        private readonly DAL.Department _contextDepartment;
        private readonly DAL.User _contextUsers;
        private readonly DAL.Position _contextPositions;
        private readonly ViewModelMapper _vmMapper;
        private readonly DalMapper _dalMapper;

        public EmployeeController(FurloughContext context, DAL.Employee contextEmployee, DAL.Department contextDepartment, DAL.User contextUsers, DAL.Position contextPositions,
            Models.Mapper.ViewModelMapper vmMapper, Models.Mapper.DalMapper dalMapper)
        {
            _context = context;
            _contextEmployee = contextEmployee;
            _contextDepartment = contextDepartment;
            _contextUsers = contextUsers;
            _contextPositions = contextPositions;

            _vmMapper = vmMapper;
            _dalMapper = dalMapper;
        }

        // GET: Admin/Employee
        public async Task<IActionResult> Index()
        {
            return View(_contextEmployee.GetAll());
        }

        // GET: Admin/Employee/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var employee = _contextEmployee.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Admin/Employee/Create
        public IActionResult Create()
        {
            ViewData["Departments"] = new SelectList(_contextDepartment.GetAll(), "Id", "Name");
            ViewData["PositionId"] = new SelectList(_contextPositions.GetAll(), "Id", "Id");
            ViewData["UserId"] = new SelectList(_contextUsers.GetUnattachedToEmployees(), "Id", "Username");
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
            ViewData["Departments"] = new SelectList(_contextDepartment.GetAll(), "Id", "Name", employee.DepartmentId);
            ViewData["PositionId"] = new SelectList(_contextPositions.GetAll(), "Id", "Id", employee.PositionId);
            ViewData["UserId"] = new SelectList(_contextUsers.GetAll(), "Id", "Id", employee.UserId);
            return View(employee);
        }

        // GET: Admin/Employee/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var employee = _contextEmployee.GetById(id);
                if (employee == null)
                {
                    return NotFound();
                }

                ViewData["Departments"] = new SelectList(_contextDepartment.GetAll(), "Id", "Name", employee.DepartmentId);
                ViewData["Positions"] = new SelectList(_contextPositions.GetAll(), "Id", "Title", employee.PositionId);
                ViewData["UserId"] = new SelectList(_contextUsers.GetAll(), "Id", "Id", employee.UserId);
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
                    if (_contextEmployee.GetById(id) == null)
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
            ViewData["Departments"] = new SelectList(_contextDepartment.GetAll(), "Id", "Name", employee.DepartmentId);
            ViewData["Positions"] = new SelectList(_contextPositions.GetAll(), "Id", "Title", employee.PositionId);
            ViewData["UserId"] = new SelectList(_contextUsers.GetAll(), "Id", "Id", employee.UserId);
            return View(employee);
        }

        // GET: Admin/Employee/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var employee = _contextEmployee.GetById(id);
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
            var employee = _contextEmployee.GetById(id);
            if (employee == null) {
                return NotFound();
            }
            var result = _contextEmployee.Delete(employee.Id);
            
            return RedirectToAction(nameof(Index));
        }
    }
}
