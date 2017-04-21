using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EZCourse.Models.Entities
{
	[ModelMetadataType(typeof(UserMetadataType))]
	public partial class User
    {
    }

	public partial class UserMetadataType
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
}
