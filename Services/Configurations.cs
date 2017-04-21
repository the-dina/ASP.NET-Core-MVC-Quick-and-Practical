using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EZCourse.Services
{
	public class SmtpOptions
	{
		public string SmtpAddress { get; set; }
		public string SmtpUsername { get; set; }
		public string SmtpPassword { get; set; }
	}

	public class ContactOptions
	{
		public string ContactFromName { get; set; }
		public string ContactFromAddress { get; set; }
		public string ContactToName { get; set; }
		public string ContactToAddress { get; set; }
	}

	public class AuthOptions
	{
		public string AuthEncryptionKey { get; set; }
	}
}
