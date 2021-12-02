using Furlough.Models.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace Furlough.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class DepartmentController : Controller
    {
        private readonly DAL.Role _contextRole;
        private readonly DAL.DepartmentPositions _contextDepartmentRoles;
        private readonly DAL.Position _contextPosition;
        private readonly DalMapper _dalMapper;
        private readonly ViewModelMapper _vmMapper;

        public DepartmentController(DAL.DepartmentPositions contextDepartmentPositions, DAL.Role contextRole, DAL.Position contextPosition,
            DalMapper dalMapper, ViewModelMapper vmMapper)
        {
            _contextRole = contextRole;
            _contextDepartmentRoles = contextDepartmentPositions;
            _contextPosition = contextPosition;

            _dalMapper = dalMapper;
            _vmMapper = vmMapper;
        }

        // GET: DepartmentRolesController
        public ActionResult Index()
        {
            var positions = new List<Models.Position>();
            var unaddedPositions = new List<Models.Position>();
            foreach (var item in _contextDepartmentRoles.GetPositionsByDepartmentId(1)) //get Department By User/Employee Id
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
    }
}
