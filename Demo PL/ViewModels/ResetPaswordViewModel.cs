using System.ComponentModel.DataAnnotations;

namespace Demo_PL.ViewModels
{
	public class ResetPaswordViewModel
	{
		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }

		[Required(ErrorMessage = "Confirm Password is required")]
		[Compare("NewPassword", ErrorMessage = "Confirm Password does not match Password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
	}
}
