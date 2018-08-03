using GuardianChecks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuardianChecks.Controllers
{
	[Authorize]
	public class NavbarController : Controller
	{
		public PartialViewResult _Navbar()
		{
			var UserId = Session["UserId"];
			ViewBag.User = UserModel.GetByUserId(int.Parse(UserId.ToString()));
			return PartialView(Navbar.GetNav(null, null).Where(x => x.Active == true));
		}
	}
}