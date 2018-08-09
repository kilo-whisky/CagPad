﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuardianChecks.Helpers
{
	public class CustomAuthorizeAttribute : AuthorizeAttribute
	{
		protected virtual CustomPrincipal CurrentUser
		{
			get { return HttpContext.Current.User as CustomPrincipal; }
		}

		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			return ((CurrentUser != null && !CurrentUser.IsInRole(Roles)) || CurrentUser == null) ? false : true;
		}

		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			RedirectToRouteResult routeData = null;

			if (CurrentUser == null)
			{
				routeData = new RedirectToRouteResult
						(new System.Web.Routing.RouteValueDictionary
						(new
						{
							controller = "Security",
							action = "Login",
						}
						));
			}
			else
			{
				routeData = new RedirectToRouteResult
				(new System.Web.Routing.RouteValueDictionary
				 (new
				 {
					 controller = "Security",
					 action = "AccessDenied"
				 }
				 ));
			}

			filterContext.Result = routeData;
		}
	}
}