using System.ComponentModel.DataAnnotations;

namespace EZCourse.Models.ViewModels
{
	public class UserManagementCreate : UserManagementChangePassword
	{
		[Required, StringLength(50, MinimumLength = 2)]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[Required, StringLength(50, MinimumLength = 2)]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Required, EmailAddress, StringLength(100)]
		public string Email { get; set; }
	}

	public class UserManagementChangePassword
	{
		[Required, StringLength(15, MinimumLength = 6)]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required, StringLength(15, MinimumLength = 6)]
		[Compare("Password")]
		[Display(Name = "Confirm Password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
	}
}
