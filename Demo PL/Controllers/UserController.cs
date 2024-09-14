using Demo_DAL.Entities;
using Demo_PL.Helpers;
using Demo_PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Demo_PL.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public UserController(UserManager<ApplicationUser> userManager)
        {
			_userManager = userManager;
		}

		public async Task<IActionResult> Index(string SearchValue)
		{
			var Users = Enumerable.Empty<ApplicationUser>().ToList();
			if (string.IsNullOrEmpty(SearchValue))
			{
				Users.AddRange(_userManager.Users);
			}
			else
			{

				Users.Add(await _userManager.FindByEmailAsync(SearchValue));
			}

			return View(Users);

		}

		//public async Task<IActionResult> Create()
		//{
		//	ViewBag.Department = await _unitOfWork.DepartmentRepository.GetAll();
		//	return View();
		//}

		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
		//{
		//	if (ModelState.IsValid)
		//	{ // sever side validation

		//		employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "images");
		//		var mapperEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
		//		var count = await _unitOfWork.EmployeeRepository.Add(mapperEmp);
		//		if (count > 0)
		//			return RedirectToAction(nameof(Index));

		//	}
		//	return View(employeeVM);
		//}

		//[HttpGet]
		// /Employee/Details/1
		// /Employee/Details
		public async Task<IActionResult> Details(string id, string viewName = "Details")
		{
			if (id == null)
				return BadRequest();
			var user = await _userManager.FindByIdAsync(id);
			if (user == null)
				return BadRequest();
			return View(viewName, user);

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
		public async Task<IActionResult> Edit([FromRoute] string id, ApplicationUser UpdatedUser)
		{
			if (id != UpdatedUser.Id)
				return BadRequest();

			if (ModelState.IsValid)
			{
				try
				{	// becouse Tracting
					var user = await _userManager.FindByIdAsync(id);
					user.UserName = UpdatedUser.UserName;
					user.PhoneNumber = UpdatedUser.PhoneNumber;
					//user.Email = UpdatedUser.Email;
					//user.SecurityStamp = UpdatedUser.SecurityStamp;
					await _userManager.UpdateAsync(user); 
					
					return RedirectToAction(nameof(Index));
				}

				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}
			}

			return View(UpdatedUser);

		}

		public async Task<IActionResult> Delete(string id)
		{
			return await Details(id, "Delete");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]

		public async Task<IActionResult> Delete([FromRoute] string id, ApplicationUser deletedUser)
		{
			if (id != deletedUser.Id)
				return BadRequest();
			try
			{		
				var user =await _userManager.FindByIdAsync(id);
				await _userManager.DeleteAsync(user);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{

				ModelState.AddModelError(string.Empty, ex.Message);
			}

			return View(deletedUser);
		}
	}
}
