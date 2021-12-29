using Furlough.Models.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace Furlough.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class DepartmentController : Controller
    {
        private readonly DAL.Role _contextRole;
        private readonly DAL.DepartmentPositions _contextDepartmentRoles;
        private readonly DAL.Position _contextPosition;
        private readonly DAL.PositionHistory _contextPositionHisotry;
        private readonly DAL.Employee _contextEmployee;
        private readonly DalMapper _dalMapper;
        private readonly ViewModelMapper _vmMapper;

        public DepartmentController(DAL.DepartmentPositions contextDepartmentPositions, DAL.Role contextRole, DAL.Position contextPosition, DAL.Employee contextEmployee,
            DAL.PositionHistory contextPositionHistory,
            DalMapper dalMapper, ViewModelMapper vmMapper)
        {
            _contextRole = contextRole;
            _contextDepartmentRoles = contextDepartmentPositions;
            _contextPosition = contextPosition;
            _contextPositionHisotry = contextPositionHistory;
            _contextEmployee = contextEmployee;

            _dalMapper = dalMapper;
            _vmMapper = vmMapper;
        }

        // GET: DepartmentRolesController
        public ActionResult Index()
        {
            //make dynamic
            var departmentId = 1;
            //make dynamic

            var positions = new List<Models.Position>();
            var unaddedPositions = new List<Models.Position>();
            var employees = new List<Models.Employee>();
            foreach (var item in _contextDepartmentRoles.GetPositionsByDepartmentId(departmentId)) //get Department By User/Employee Id
            {
                positions.Add(_vmMapper.PositionMap(_contextPosition.GetById(item.PositionId)));
            }
            foreach (var item in _contextPosition.GetAll())
            {
                var vmItem = _vmMapper.PositionMap(item);
                if (!positions.Exists(x=> x.Id == vmItem.Id))
                {
                    unaddedPositions.Add(vmItem);
                }
            }
            foreach (var item in _contextEmployee.GetByDepartmentId(departmentId))
            {
                employees.Add(_vmMapper.EmployeeMap(item));
            }

            ViewBag.employees = employees;
            ViewBag.positions = positions;
            ViewBag.unaddedPositions = unaddedPositions;
            return View();
        }

        // GET: DepartmentRolesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DepartmentRolesController/Create
        public ActionResult Create()
        {
            ViewBag.Roles = _contextRole.GetAll();
            return View();
        }

        // POST: DepartmentRolesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DepartmentRolesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DepartmentRolesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DepartmentRolesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DepartmentRolesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPut]
        public bool UpdateDepartmentPositions(int departmentId, IEnumerable<string> positionsId)
        {
            departmentId = 1;
            var alreadyAddedPositions = new List<string>();
            foreach (var item in _contextDepartmentRoles.GetPositionsByDepartmentId(departmentId))
            {
                alreadyAddedPositions.Add(item.PositionId.ToString());
            }

            var positionsToAdd = "";
            for (var i = 0; i < positionsId.Count(); i++)
            {
                //if (alreadyAddedPositions.Contains(positionsId.ElementAt(i)))
                //    continue;

                if (i < positionsId.Count() - 1)
                {
                    positionsToAdd += $"{positionsId.ElementAt(i)},";
                }
                else
                    positionsToAdd += $"{positionsId.ElementAt(i)}";
            }
            
            //We don't connect Employee with DepartmentPositions
            //because there might've been a position before that now no longer exists in a specific department
            var result = _contextDepartmentRoles.UpdateDepartmentPositions(departmentId, positionsToAdd);
            return true;
        }

        [HttpPut]
        public bool UpdateEmployeePosition(int employeeId, int positionId)
        {
            try
            {
                var historyResult = _contextPositionHisotry.Add(new DAL.Models.PositionHistory
                {
                    EmployeeId = employeeId,
                    PositionId = positionId,
                    SetByUserId = 1 //SET USER ID HERE
                });
                var dbEmployee = _contextEmployee.GetById(employeeId);
                if (dbEmployee == null) return false;

                dbEmployee.PositionId = positionId;
                var result = _contextEmployee.Edit(dbEmployee);
                return result;
            }
            catch (Exception e)
            {

                return false;
            }
        }
    }
}
