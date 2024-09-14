using AutoMapper;
using Demo_BLL.Interfaces;
using Demo_BLL.Repositories;
using Demo_DAL.Entities;
using Demo_PL.Helpers;
using Demo_PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Demo_PL.Controllers
{
	[Authorize]

	public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index(string SearchValue)
        {
            var Employees = Enumerable.Empty<Employee>();
            if (string.IsNullOrEmpty(SearchValue))
            {
                 Employees = await _unitOfWork.EmployeeRepository.GetAll();
            }
            else {

                Employees =_unitOfWork.EmployeeRepository.SearchEmployeesByName(SearchValue);
            }

            var mapperEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(Employees);
            return View(mapperEmp);

        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Department =await _unitOfWork.DepartmentRepository.GetAll();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            { // sever side validation

               employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "images");
                var mapperEmp= _mapper.Map<EmployeeViewModel,Employee>( employeeVM );
                var count =await _unitOfWork.EmployeeRepository.Add(mapperEmp);
                if (count > 0)
                    return RedirectToAction(nameof(Index));
                
            }
            return View(employeeVM);
        }

        //[HttpGet]
        // /Employee/Details/1
        // /Employee/Details
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id == null)
                return BadRequest();
            var employee = await _unitOfWork.EmployeeRepository.Get(id.Value);
            if (employee == null)
                return BadRequest();

            var mapperEmp =_mapper.Map<Employee, EmployeeViewModel>(employee);
            return View(viewName, mapperEmp);

        }

        //[HttpGet]
        // /Employee/Eidt/1
        // /Employee/Eidt
        public async Task<IActionResult> Edit(int? id)
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
        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var mapperEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                  await  _unitOfWork.EmployeeRepository.Update(mapperEmp);
                    return RedirectToAction(nameof(Index));
                }

                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(employeeVM);

        }

        public async Task<IActionResult> Delete(int id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            try
            {
             
                var mapperEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                int count =await _unitOfWork.EmployeeRepository.Delete(mapperEmp);
                if (count > 0)
                    DocumentSettings.DeleteFile(employeeVM.ImageName, "images");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(employeeVM);
        }
    }
}
