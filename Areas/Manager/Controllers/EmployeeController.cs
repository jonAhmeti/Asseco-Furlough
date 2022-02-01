﻿using System;
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
        private readonly DAL.Employee _contextEmployee;
        private readonly DAL.Position _contextPosition;
        private readonly DAL.User _contextUsers;
        private readonly DAL.Department _contextDepartments;
        private readonly DAL.DepartmentPositions _contextDepartmentPosition;
        private readonly DAL.AvailableDays _contextAvailableDays;
        private readonly DAL.FurloughContext _context;

        public EmployeeController(ViewModelMapper vmMapper,
            DAL.Employee contextEmployee, DAL.Position contextPosition, DAL.User contextUsers,
            DAL.Department contextDepartments, DAL.DepartmentPositions contextDepartmentPosition,
            DAL.AvailableDays contextAvailableDays, DAL.FurloughContext context)
        {
            _vmMapper = vmMapper;

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
            var departmentId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Department").Value;
            var employees = _contextEmployee.GetByDepartmentId(int.Parse(departmentId));

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
        public IActionResult Create()
        {
            var departmentId = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Department").Value);
            var departmentPositions = new List<Models.Position>();
            foreach (var item in _contextDepartmentPosition.GetPositionsByDepartmentId(departmentId))
            {
                departmentPositions.Add(_vmMapper.PositionMap(_contextPosition.GetById(item.PositionId)));
            }

            var unattachedUsers = _contextUsers.GetUnattachedToEmployees();
            ViewData["Users"] = unattachedUsers.Count() == 0 ? null : new SelectList(unattachedUsers, "Id", "Username");
            ViewBag.DepartmentPositions = new SelectList(departmentPositions, "Id", "Title");
            return View();
        }

        // POST: Manager/Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,JoinDate,PositionId,DepartmentId,Email,Name")] DAL.Models.Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var departmentId = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Department").Value);
            var departmentPositions = new List<Models.Position>();
            foreach (var item in _contextDepartmentPosition.GetPositionsByDepartmentId(departmentId))
            {
                departmentPositions.Add(_vmMapper.PositionMap(_contextPosition.GetById(item.PositionId)));
            }
            var unattachedUsers = _contextUsers.GetUnattachedToEmployees();
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
            return View(employee);
        }

        // POST: Manager/Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,JoinDate,PositionId,DepartmentId,Email,Name")] DAL.Models.Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid) //This model should be a ViewModel instead of DAL.Models.Employee
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
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
                var result = _contextEmployee.Delete(id);
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
    }
}
