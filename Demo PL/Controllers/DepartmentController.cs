using Demo_BLL.Interfaces;
using Demo_DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

namespace Demo_PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();
            return View(departments);
        }

        public IActionResult Create ()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid) { // sever side validation
            
                _departmentRepository.Add(department);
                return RedirectToAction("Index");
            }
            return View(department);
        }

        //[HttpGet]
        // /Department/Details/1
        // /Department/Details
        public IActionResult Details(int? id, string viewName = "Details") 
        {
            if (id==null)
                return BadRequest();
            var department = _departmentRepository.Get(id.Value);
            if (department == null)
                return BadRequest();
            return View(viewName, department);
        
        }

        //[HttpGet]
        // /Department/Eidt/1
        // /Department/Eidt
        public IActionResult Edit(int? id) 
        {
            return Details(id,"Edit");
            ///if(id==null)
            ///    return BadRequest();
            ///var department = _departmentRepository.Get(id.Value);
            ///if(department == null)
            ///    return BadRequest();
            ///return View(department);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id ,Department department) 
        {
            if(id != department.Id)
                return BadRequest();

            if (ModelState.IsValid) {
                try {
                    _departmentRepository.Update(department);
                    return RedirectToAction(nameof(Index));
                }

                catch(Exception ex) 
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }   
             }

            return View(department);

        }

        public IActionResult Delete(int id) 
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Delete([FromRoute]int id,Department department) 
        {
            if(id != department.Id)
                return BadRequest();
            try
            {
                _departmentRepository.Delete(department);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(department);
        }
    }
}
