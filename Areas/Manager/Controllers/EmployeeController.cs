using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Furlough.DAL.Models;
using System.Globalization;
using Furlough.Models.Mapper;

namespace Furlough.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class EmployeeController : Controller
    {
        private readonly ViewModelMapper _vmMapper;
        private readonly DalMapper _dalMapper;
        private readonly DAL.Employee _contextEmployee;
        private readonly DAL.Position _contextPosition;
        private readonly DAL.User _contextUsers;
        private readonly DAL.Department _contextDepartments;
        private readonly DAL.DepartmentPositions _contextDepartmentPosition;
        private readonly DAL.AvailableDays _contextAvailableDays;
        private readonly DAL.FurloughContext _context;

        public EmployeeController(ViewModelMapper vmMapper, DalMapper dalMapper,
            DAL.Employee contextEmployee, DAL.Position contextPosition, DAL.User contextUsers,
            DAL.Department contextDepartments, DAL.DepartmentPositions contextDepartmentPosition,
            DAL.AvailableDays contextAvailableDays, DAL.FurloughContext context)
        {
            _vmMapper = vmMapper;
            _dalMapper = dalMapper;

            _contextEmployee = contextEmployee;
            _contextPosition = contextPosition;
            _contextUsers = contextUsers;
            _contextDepartments = contextDepartments;
            _contextDepartmentPosition = contextDepartmentPosition;
            _contextAvailableDays = contextAvailableDays;

            _context = context;
        }

        // GET: Manager/Employee
        public async Task<IActionResult> Index()
        {
            var departmentId = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Department").Value);
            var employees = _contextEmployee.GetByDepartmentId(departmentId);

            ViewData["Positions"] = _contextDepartmentPosition.GetPositionsByDepartmentId(departmentId); 
            return View(employees);
        }

        // GET: Manager/Employee/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var employee = _contextEmployee.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }

            ViewData["EmployeeDays"] = _contextAvailableDays.GetByEmployeeId(employee.Id);

            return View(employee);
        }

        // GET: Manager/Employee/Create
        public IActionResult Create(string? message = null)
        {
            var departmentId = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Department").Value);
            var departmentPositions = new List<Models.Position>();
            foreach (var item in _contextDepartmentPosition.GetPositionsByDepartmentId(departmentId))
            {
                departmentPositions.Add(_vmMapper.PositionMap(_contextPosition.GetById(item.PositionId)));
            }

            var unattachedUsers = _contextUsers.GetUnattachedToEmployees();
            ViewData["Message"] = message;
            ViewData["Users"] = unattachedUsers.Count() == 0 ? null : new SelectList(unattachedUsers, "Id", "Username");
            ViewBag.DepartmentPositions = new SelectList(departmentPositions, "Id", "Title");
            return View();
        }

        // POST: Manager/Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.Employee employee)
        {
            string? message = null;
            if (ModelState.IsValid)
            {
                var loggedinUser = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "User").Value);
                var loggedinDepartment = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Department").Value);
                var dalEmployee = _dalMapper.DalEmployeeMap(employee);
                dalEmployee.LUBUserId = loggedinUser;
                dalEmployee.DepartmentId = loggedinDepartment;

                var employeeId = _contextEmployee.Add(dalEmployee);
                if (employeeId == null)
                {
                    message = "Something went wrong adding a new employee. Make sure the email isn't already in use.";
                    return RedirectToAction(nameof(Create), new { message = message });
                }

                var result = _contextAvailableDays.SetAllDays(employeeId.Value, CalculateYearlyDays(employee.WorkStartDate));
                return RedirectToAction(nameof(Index));
            }

            var departmentId = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Department").Value);
            var departmentPositions = new List<Models.Position>();
            foreach (var item in _contextDepartmentPosition.GetPositionsByDepartmentId(departmentId))
            {
                departmentPositions.Add(_vmMapper.PositionMap(_contextPosition.GetById(item.PositionId)));
            }
            var unattachedUsers = _contextUsers.GetUnattachedToEmployees();

            ViewData["Message"] = message;
            ViewData["Users"] = unattachedUsers.Count() == 0 ? null : new SelectList(unattachedUsers, "Id", "Username");
            ViewBag.DepartmentPositions = new SelectList(departmentPositions, "Id", "Title");
            return View(employee);
        }

        // GET: Manager/Employee/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var employee = _contextEmployee.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }

            var departmentId = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Department").Value);
            var departmentPositions = new List<Models.Position>();
            foreach (var item in _contextDepartmentPosition.GetPositionsByDepartmentId(departmentId))
            {
                departmentPositions.Add(_vmMapper.PositionMap(_contextPosition.GetById(item.PositionId)));
            }
            ViewBag.DepartmentPositions = new SelectList(departmentPositions, "Id", "Title");
            return View(_vmMapper.EmployeeMap(employee));
        }

        // POST: Manager/Employee/Edit/5
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
            //var prevEmployee = _contextEmployee.GetById(id);

            if (ModelState.IsValid)
            {
                var loggedinUser = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "User").Value);

                try
                {
                    employee.LUBUserId = loggedinUser;
                    var result = _contextEmployee.Edit(_dalMapper.DalEmployeeMap(employee));
                }
                catch (Exception e)
                {
                    if (e.Message.Contains("UNIQUE KEY"))
                    {
                        return BadRequest("Email is already in use.");
                    }
                    if (_contextEmployee.GetById(id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(e.Message);
                        Console.ResetColor();
                        return StatusCode(500, "Something went wrong.");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var departmentId = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Department").Value);
            var departmentPositions = new List<Models.Position>();
            foreach (var item in _contextDepartmentPosition.GetPositionsByDepartmentId(departmentId))
            {
                departmentPositions.Add(_vmMapper.PositionMap(_contextPosition.GetById(item.PositionId)));
            }
            ViewBag.DepartmentPositions = new SelectList(departmentPositions, "Id", "Title");
            return View(employee);
        }

        // GET: Manager/Employee/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var employee = _contextEmployee.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(_vmMapper.EmployeeMap(employee));
        }

        // POST: Manager/Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var deleteAvailableDays = _contextAvailableDays.Delete(id);
                bool result = false;
                if (deleteAvailableDays)
                    result = _contextEmployee.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();
                return RedirectToAction(nameof(Delete), id);
            }
        }

        public int CalculateYearlyDays(DateTime workStartDate)
        {
            var presentEOY = new DateTime(DateTime.Now.Year, 12, 31);
            var yearsWorking = presentEOY.Year - workStartDate.Year;

            var availableDays = 0;
            double availableDaysByMonth = 0;
            for (int workYear = 0; workYear <= yearsWorking; workYear++)
            {
                if (workYear == 0) //1.5 monthly
                {
                    availableDays = 18;

                    const double coefficient = 1.5;
                    for (int workMonth = 1; workMonth <= 12; workMonth++)
                    {
                        if (workMonth > workStartDate.Month && workMonth <= DateTime.Now.Month)
                        {
                            availableDaysByMonth = workMonth * coefficient;
                        }
                    }
                }
                else if (workYear == 1)
                {
                    availableDays = 20;

                    const double coefficient = 1.66;
                    for (int workMonth = 1; workMonth <= 12; workMonth++)
                    {
                        if (workMonth > workStartDate.Month && workMonth <= DateTime.Now.Month)
                        {
                            availableDaysByMonth = workMonth * coefficient;
                        }
                        else
                        {
                            availableDaysByMonth = workMonth * 1.5;
                        }
                    }
                }
                else if (workYear == 6)
                {
                    availableDays = 21;
                    const double coefficient = 1.75;
                }
                else if (workYear % 5 == 0)
                {
                    availableDays++;
                }
            }

            return availableDays; //at this point in the code
                                  //yearCounter = years working
                                  //span.Days = days so far not counted into yearly leave
                                  //yearlyDaysAllowed = the amount of yearly leave days available for this employee
                                  //the "for loop" is also supposed to manage leap years
        }
    }
}
