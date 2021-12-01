using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Furlough.DAL;
using Furlough.DAL.Models;

namespace Furlough.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly FurloughContext _context;
        private readonly DAL.User _contextUser;
        private readonly Models.Mapper.DalMapper _dalMapper;
        private readonly Models.Mapper.ViewModelMapper _vmMapper;
        public UserController(FurloughContext context, DAL.User contextUser,
            Models.Mapper.DalMapper dalMapper, Models.Mapper.ViewModelMapper vmMapper)
        {
            _context = context;
            _contextUser = contextUser;
            _dalMapper = dalMapper;
            _vmMapper = vmMapper;
        }

        // GET: Admin/User
        public async Task<IActionResult> Index()
        {
            var users = new List<Models.User>();
            foreach (var item in _contextUser.GetAll())
            {
                users.Add(_vmMapper.UserMap(item));
            }

            return View(users);
        }

        // GET: Admin/User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _vmMapper.UserMap(_contextUser.GetById(id.Value));

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Admin/User/Create
        public IActionResult Create()
        {
            ViewData["Roles"] = new SelectList(_context.Roles, "Id", "Title");
            return View();
        }

        // POST: Admin/User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.User user)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(user);
                //await _context.SaveChangesAsync();
                _contextUser.Add(_dalMapper.DalUserMap(user));
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", user.RoleId);
            ViewData["UpdateBy"] = new SelectList(_context.Users, "Id", "Id", user.UpdateBy);
            return View(user);
        }

        // GET: Admin/User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _vmMapper.UserMap(_contextUser.GetById(id.Value));
            if (user == null)
            {
                return NotFound();
            }

            ViewData["Roles"] = new SelectList(_context.Roles, "Id", "Title", user.RoleId);
            ViewData["UpdateBy"] = new SelectList(_context.Users, "Id", "Id", user.UpdateBy);
            return View(user);
        }

        // POST: Admin/User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Models.User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(user);
                    //await _context.SaveChangesAsync();
                    var result = _contextUser.Edit(_dalMapper.DalUserMap(user));
                }
                catch (DbUpdateConcurrencyException e)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error in UserController of Admin Area onEdit: " + e.Message);
                        Console.ResetColor();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Roles"] = new SelectList(_context.Roles, "Id", "Title", user.RoleId);
            ViewData["UpdateBy"] = new SelectList(_context.Users, "Id", "Id", user.UpdateBy);
            return View(user);
        }

        // GET: Admin/User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .Include(u => u.UpdateByNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Admin/User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var user = await _context.Users.FindAsync(id);
            //_context.Users.Remove(user);
            //await _context.SaveChangesAsync();
            try
            {
                var result = _contextUser.Delete(id);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error in UserController of Admin Area onDelete: " + e.Message);
                Console.ResetColor();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
