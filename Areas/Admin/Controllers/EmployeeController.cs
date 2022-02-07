using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Furlough.Models.Mapper;

namespace Furlough.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private readonly DAL.Employee _contextEmployee;
        private readonly DAL.Department _contextDepartment;
        private readonly DAL.User _contextUsers;
        private readonly DAL.DepartmentPositions _contextDepartmentPositions;
        private readonly DAL.AvailableDays _contextAvailableDays;
        private readonly DAL.Request _contextRequest;
        private readonly ViewModelMapper _vmMapper;
        private readonly DalMapper _dalMapper;

        public EmployeeController(DAL.Employee contextEmployee, DAL.Department contextDepartment, DAL.User contextUsers,
            DAL.AvailableDays contextAvailableDays, DAL.DepartmentPositions contextDepartmentPositions, DAL.Request contextRequest,
            ViewModelMapper vmMapper, DalMapper dalMapper)
        {
            _contextEmployee = contextEmployee;
            _contextDepartment = contextDepartment;
            _contextUsers = contextUsers;
            _contextDepartmentPositions = contextDepartmentPositions;
            _contextAvailableDays = contextAvailableDays;
            _contextRequest = contextRequest;

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

            ViewData["EmployeeDays"] = _contextAvailableDays.GetByEmployeeId(employee.Id);
            
            return View(employee);
        }

        // GET: Admin/Employee/Create
        public IActionResult Create()
        {
            ViewData["Departments"] = new SelectList(_contextDepartment.GetAll(), "Id", "Name");
            //get positions by department on ajax

            var unattachedUsers = _contextUsers.GetUnattachedToEmployees();
            ViewData["Users"] = unattachedUsers.Count() == 0 ? null : new SelectList(unattachedUsers, "Id", "Username");
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
                var loggedinUser = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "User").Value);
                employee.LUBUserId = loggedinUser;

                var employeeId = _contextEmployee.Add(_dalMapper.DalEmployeeMap(employee));
                if (employeeId == null)
                {
                    return StatusCode(500, "Something went wrong adding a new employee.");
                }

                var result = _contextAvailableDays.SetAllDays(employeeId.Value, CalculateYearlyDays(employee.WorkStartDate));
                return RedirectToAction(nameof(Index));
            }

            ViewData["Departments"] = new SelectList(_contextDepartment.GetAll(), "Id", "Name", employee.DepartmentId);
            //get positions by department on ajax
            var unattachedUsers = _contextUsers.GetUnattachedToEmployees();
            ViewData["Users"] = unattachedUsers.Count() == 0 ? null : new SelectList(unattachedUsers, "Id", "Username");
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
                //get positions by department on ajax
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
                    var loggedinUser = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "User").Value);
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
            ViewData["Departments"] = new SelectList(_contextDepartment.GetAll(), "Id", "Name", employee.DepartmentId);
            //get positions by department on ajax
            return View(employee);
        }

        // GET: Admin/Employee/Delete/5
        public async Task<IActionResult> Delete(int id, string? message = null)
        {
            var employee = _contextEmployee.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }

            ViewData["Message"] = message;
            return View(employee);
        }

        //POST: Admin/Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string? message = null;

            var employeeDb = _contextEmployee.GetById(id);
            if (employeeDb == null)
                return NotFound();

            try
            {
                if (_contextRequest.GetByUser(employeeDb.UserId).Any())
                {
                    message = "Employee can't be delete because they have existing requests";
                    return RedirectToAction(nameof(Delete), new { id, message });
                }

                var availDaysDelete = _contextAvailableDays.Delete(id);
                if (availDaysDelete)
                {
                    var result = _contextEmployee.Delete(id);
                    return RedirectToAction(nameof(Index));
                }

                message = "Employee's available days couldn't be deleted";
                return RedirectToAction(nameof(Delete), new { id, message });
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();
                return RedirectToAction(nameof(Delete), id);
            }
        }

        // POST: Admin/Employee/Reset/5
        public async Task<IActionResult> Reset(int id)
        {
            var employee = _contextEmployee.GetById(id);
            if (employee == null)
                return NotFound("Employee not found.");

            //  Reset employee available days to default and yearlyDays to the given value from method
            var result = _contextAvailableDays.SetAllDays(employee.Id, CalculateYearlyDays(employee.WorkStartDate));
            return result ? Ok() : StatusCode(500);
        }


        //Calculates yearly days available starting from workStartDate
        #region yearlyDaysCalculation
        //recently added allowedDays variable for testing, testing will be done in the Test method below other times
        public int[] CalculateYearlyDaysTest(DateTime workStartDate)
        {
            var endOfYear = new DateTime(DateTime.Now.Year, 12, 31);
            var span = endOfYear.Subtract(workStartDate);
            int yearlyDaysAllowed = 18;
            float allowedDays = 0; //test

            var yearCounter = 0;
            for (int i = 0; i <= endOfYear.Year - workStartDate.Year; i++)
            {
                if (span.Days <= 0)
                    return new int[] { yearCounter, span.Days, yearlyDaysAllowed };

                var date = new DateTime(workStartDate.Year + i, workStartDate.Month, 1); //workStartDate anniversary
                var thisDateEndOfYear = new DateTime(workStartDate.Year + i, 12, 31); //test
                var nextDate = new DateTime(workStartDate.Year + i + 1, workStartDate.Month, 1);

                var thatYearsWorkDays = thisDateEndOfYear - date;   //test

                span = span.Subtract(new TimeSpan(days: thatYearsWorkDays.Days, 0, 0, 0));

                if (yearCounter == 0) //this if might be obsolete bc of the for loop's condition
                {
                    yearlyDaysAllowed = 18;
                    allowedDays = (float)(thatYearsWorkDays.TotalDays / (thisDateEndOfYear.DayOfYear / 12) * 1.5);
                    Console.WriteLine(allowedDays);
                }
                else if (yearCounter == 1)
                {
                    yearlyDaysAllowed = 20;
                    allowedDays = (float)(thatYearsWorkDays.TotalDays / (thisDateEndOfYear.DayOfYear / 12) * 1.66);
                    Console.WriteLine(allowedDays);
                }
                else if (yearCounter == 6)
                {
                    yearlyDaysAllowed = 21;
                    allowedDays = (float)(thatYearsWorkDays.TotalDays / (thisDateEndOfYear.DayOfYear / 12) * 1.75);
                    Console.WriteLine(allowedDays);
                }
                else if (yearCounter % 5 == 0)
                {
                    yearlyDaysAllowed++;
                    allowedDays = (float)((thatYearsWorkDays.TotalDays / (thisDateEndOfYear.DayOfYear / 12) * 1.75)) + i;
                    allowedDays += (float)((thisDateEndOfYear.DayOfYear - thatYearsWorkDays.TotalDays) / (thisDateEndOfYear.DayOfYear / 12) * 1.75) + i - 1;
                    Console.WriteLine(allowedDays);
                }

                yearCounter++;
            }

            return new int[] { yearCounter, span.Days, yearlyDaysAllowed }; //at this point in the code
                                                                            //yearCounter = years working
                                                                            //span.Days = days so far not counted into yearly leave
                                                                            //yearlyDaysAllowed = the amount of yearly leave days available for this employee
                                                                            //the "for loop" is also supposed to manage leap years
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
        #endregion
    }
}
