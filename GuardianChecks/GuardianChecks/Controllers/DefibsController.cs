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
			return View(Defib.GetDefibs(null, null));
		}

		public ActionResult Details(int DefibId)
		{
			return View(Defib.GetDefibs(DefibId, null).First());
		}

		public ActionResult Add()
		{
			Defib d = new Defib();
			return View(d);
		}

		[HttpPost]
		public ActionResult Add(Defib d)
		{
			int DefibId = d.upsert();
			return RedirectToAction("Details", "Defibs", new { DefibId });
		}

		public ActionResult Edit(int DefibId)
		{
			return View(Defib.GetDefibs(DefibId, null)[0]);
		}

		[HttpPost]
		public ActionResult Edit(Defib d)
		{
			d.upsert();
			return RedirectToAction("Details", "Defibs", new { d.DefibId });
		}

		public PartialViewResult _Issues(int DefibId)
		{
			return PartialView(Issue.GetIssues(null, null, false, null, DefibId, null));
		}
	}
}