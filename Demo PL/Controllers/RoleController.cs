using Demo_DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Demo_PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            var Roles = Enumerable.Empty<IdentityRole>().ToList();
            if (string.IsNullOrEmpty(SearchValue))
            {
                Roles.AddRange(_roleManager.Roles);
            }
            else
            {

                Roles.Add(await _roleManager.FindByNameAsync(SearchValue));
            }

            return View(Roles);

        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            if (ModelState.IsValid)
            { // sever side validation

                await _roleManager.CreateAsync(role);   
                    return RedirectToAction(nameof(Index));

            }
            return View(role);
        }

        //[HttpGet]
        // /Employee/Details/1
        // /Employee/Details
        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id == null)
                return BadRequest();
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return BadRequest();
            return View(viewName, role);

        }

        //[HttpGet]
        // /Employee/Eidt/1
        // /Employee/Eidt
        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
            ///if(id==null)
            ///    return BadRequest();
            ///var department = _departmentRepository.Get(id.Value);
            ///if(department == null)
            ///    return BadRequest();
            ///return View(department);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, IdentityRole UpdateRole)
        {
            if (id != UpdateRole.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _roleManager.FindByIdAsync(id);
                    user.Name = UpdateRole.Name;
                    await _roleManager.UpdateAsync(user);

                    return RedirectToAction(nameof(Index));
                }

                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(UpdateRole);

        }

        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete([FromRoute] string id, IdentityRole deletedRole)
        {
            if (id != deletedRole.Id)
                return BadRequest();
            try
            {
                var user = await _roleManager.FindByIdAsync(id);
                await _roleManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(deletedRole);
        }
    }
}
