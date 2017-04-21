using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EZCourse.Filters
{
	public class EZAuthAttribute : ActionFilterAttribute, IAuthorizationFilter
	{
		readonly List<string> _permissions;

		public EZAuthAttribute(string permissions = "")
		{
			_permissions = permissions.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
				.Select(p => p.Trim())
				.ToList();
		}

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			var service = context.HttpContext.RequestServices.GetService(typeof(Services.EZAuth)) as Services.EZAuth;

			if (service.ScopeAuthInfo.IsAuthenticated && !_permissions.Any())
			{
				return;
			}

			if (!service.ScopeAuthInfo.IsAuthenticated || !_permissions.Any(p => service.ScopeAuthInfo.Permissions.Contains(p)))
			{
				var returnUrl = context.HttpContext.Request.Path;
				context.Result = new RedirectToActionResult("Login", "Account", new { returnUrl = returnUrl });
			}
		}
	}
}
