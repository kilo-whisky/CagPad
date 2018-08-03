using GuardianChecks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuardianChecks.Controllers
{
	[Authorize]
	public class DefibsController : Controller
	{
		// GET: Defibs
		public ActionResult Index()
		{
			return View(Defib.GetDefibs(null));
		}

		public ActionResult Details(int DefibId)
		{
			return View(Defib.GetDefibs(DefibId).First());
		}

	}
}