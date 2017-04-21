using EZCourse.Models.Entities;
using System;
using System.Collections.Generic;

namespace EZCourse.Models
{
	public class AuthInfo
	{
		public AuthInfo()
		{
			CreationDate = DateTime.UtcNow;
		}
		public int UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public List<string> Permissions { get; set; }
		public Dictionary<string, string> Claims { get; set; }
		public DateTime CreationDate { get; set; }

		public bool IsAuthenticated => (UserId > 0);
	}
}
