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
		[Authorize(Roles = "SYSADMIN,READER")]
		public ActionResult Index()
		{
			return View(Defib.GetDefibs(null, null));
		}

		[Authorize(Roles = "SYSADMIN,READER")]
		public ActionResult Details(int DefibId)
		{
			return View(Defib.GetDefibs(DefibId, null).First());
		}

		[Authorize(Roles = "SYSADMIN")]
		public ActionResult Add()
		{
			Defib d = new Defib();
			return View(d);
		}

		[Authorize(Roles = "SYSADMIN")]
		[HttpPost]
		public ActionResult Add(Defib d)
		{
			int DefibId = d.upsert();
			return RedirectToAction("Details", "Defibs", new { DefibId });
		}

		[Authorize(Roles = "SYSADMIN")]
		public ActionResult Edit(int DefibId)
		{
			return View(Defib.GetDefibs(DefibId, null)[0]);
		}

		[Authorize(Roles = "SYSADMIN")]
		[HttpPost]
		public ActionResult Edit(Defib d)
		{
			d.upsert();
			return RedirectToAction("Details", "Defibs", new { d.DefibId });
		}

		[Authorize(Roles = "SYSADMIN,READER")]
		public PartialViewResult _Issues(int DefibId)
		{
			return PartialView(Issue.GetIssues(null, null, false, null, DefibId, null, true));
		}
	}
}