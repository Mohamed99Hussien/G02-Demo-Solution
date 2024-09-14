using Demo_DAL.Entities;
using Demo_PL.Helpers;
using Demo_PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo_PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
		#region Register
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser
				{
					UserName = model.Email.Split('@')[0],
					Email = model.Email,
					IsAgree = model.IsAgree,
				};

				var result = await _userManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
					return RedirectToAction(nameof(Login));
				foreach (var error in result.Errors)
					ModelState.AddModelError(string.Empty, error.Description);
			}
			return View(model);

		}

		#endregion

		#region Login
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user != null)
				{
					bool flag = await _userManager.CheckPasswordAsync(user, model.Password);
					if (flag)
					{
						var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
						if (result.Succeeded)
							return RedirectToAction("Index", "Home");
					}
					ModelState.AddModelError(string.Empty, "Password is not correct");
				}

				ModelState.AddModelError(string.Empty, "Email is Not Existed");
			}
			return View(model);

		}

		#endregion

		#region SignOut
		public async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(Login));
		}
		#endregion

		#region ForgetPassword
		public IActionResult ForgetPassword()
		{
			return View();
		}

		public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model) 
		{
			if (!ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user != null)
				{
				var token = await _userManager.GeneratePasswordResetTokenAsync(user);
					var resetPasswordLink = Url.Action("ResetPassword", "Account", new {Email= model.Email,Token=token },Request.Scheme);
					var email = new Email()
					{
						Subject = "Reset Your Password",
						To = model.Email,
						Body = resetPasswordLink
					};

					EmailSettings.SendEmail(email);
					return RedirectToAction(nameof(CheckYourInBox));
				}
				ModelState.AddModelError(string.Empty, "Email is Not Existed");
			}
			return View(model);
		}

		public IActionResult CheckYourInBox()
		{	
			return View();
		}
		#endregion

		#region ResetPassword
		public IActionResult ResetPassword(string email,string token) 
		{
			TempData["Email"]=email;
			TempData["Token"]=token;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPaswordViewModel model) 
		{
			if (ModelState.IsValid)
			{
				string Email = TempData["Email"] as string;
				string Token = TempData["token"] as string;

				var user =await _userManager.FindByEmailAsync(Email);

			 var result=await _userManager.ResetPasswordAsync(user,Token,model.NewPassword);
				if (result.Succeeded)
					return RedirectToAction(nameof(Login));
				foreach (var error in result.Errors)
					ModelState.AddModelError(string.Empty, error.Description);
			}
			return View(model);
		}
		#endregion

	}
}
