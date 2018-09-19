using GuardianChecks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuardianChecks.Controllers
{
	[Authorize]
	public class CabinetsController : Controller
	{
		// GET: Cabinets
		[Authorize(Roles = "SYSADMIN,READER")]
		public ActionResult Index()
		{
			return View(Cabinet.GetCabinets(null, null));
		}

		[Authorize(Roles = "SYSADMIN,READER")]
		public ActionResult Details(int CabinetId)
		{
			return View(Cabinet.GetCabinets(CabinetId, null).First());
		}

		[Authorize(Roles = "SYSADMIN")]
		public ActionResult Add()
		{
			Cabinet c = new Cabinet();
			return View(c);
		}

		[Authorize(Roles = "SYSADMIN")]
		[HttpPost]
		public ActionResult Add(Cabinet c)
		{
			int CabinetId = c.upsert();
			return RedirectToAction("Details", "Cabinets", new { CabinetId });
		}

		[Authorize(Roles = "SYSADMIN")]
		public ActionResult Edit(int CabinetId)
		{
			return View(Cabinet.GetCabinets(CabinetId, null)[0]);
		}

		[Authorize(Roles = "SYSADMIN")]
		[HttpPost]
		public ActionResult Edit(Cabinet c)
		{
			c.upsert();
			return RedirectToAction("Details", "Cabinets", new { c.CabinetId });
		}

		[Authorize(Roles = "SYSADMIN,READER")]
		public PartialViewResult _Issues(int CabinetId)
		{
			return PartialView(Issue.GetIssues(null, null, false, null, null, CabinetId, true));
		}
	}
}