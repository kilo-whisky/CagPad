using GuardianChecks.Helpers;
using GuardianChecks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuardianChecks.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		
		public ActionResult Index()
		{
			return View();
		}

		public PartialViewResult _Equipment()
		{
			return PartialView(Equipment.GetEquipment(null).Where(x => x.Expiry != null).Take(5));
		}

		public PartialViewResult _Issues()
		{
			return PartialView(Issue.GetIssues(null, null, false, null, null, null).Where(x => !x.Resolved).ToList());
		}
	}
}