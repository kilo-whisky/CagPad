using GuardianChecks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuardianChecks.Controllers
{
	[Authorize]
	public class PADSitesController : Controller
	{
		// GET: PADSites
		[Authorize(Roles = "SYSADMIN,READER")]
		public ActionResult Index()
		{
			return View(PAD.GetPadSites(null));
		}

		[Authorize(Roles = "SYSADMIN,READER")]
		public ActionResult Site(int PadId)
		{
			return View(PAD.GetPadSites(PadId).First());
		}

		[Authorize(Roles = "SYSADMIN,READER")]
		public PartialViewResult _Equipment(int PadId)
		{
			return PartialView(Equipment.GetEquipment(PadId));
		}

		[Authorize(Roles = "SYSADMIN,READER")]
		public PartialViewResult _Checks(int PadId)
		{
			return PartialView(GuardianCheck.GetChecks(null, PadId, null));
		}

		[Authorize(Roles = "SYSADMIN")]
		public PartialViewResult _Guardians (int PadId)
		{
			return PartialView();
		}

		[Authorize(Roles = "SYSADMIN")]
		public ActionResult Add()
		{
			PAD p = new PAD();
			ViewBag.Cabinets = Cabinet.GetCabinets(null, false);
			ViewBag.Defibs = Defib.GetDefibs(null, false);
			return View(p);
		}

		[Authorize(Roles = "SYSADMIN")]
		[HttpPost]
		public ActionResult Add(PAD p)
		{
			int PadId = p.upsert();
			return RedirectToAction("Site", "PADSites", new { PadId });
		}

		[Authorize(Roles = "SYSADMIN")]
		public ActionResult Edit(int PadId)
		{
			ViewBag.Cabinets = Cabinet.GetCabinets(null, false);
			ViewBag.Defibs = Defib.GetDefibs(null, false);
			return View(PAD.GetPadSites(PadId)[0]);
		}

		[Authorize(Roles = "SYSADMIN")]
		[HttpPost]
		public ActionResult Edit(PAD p)
		{
			p.upsert();
			return RedirectToAction("Site", "PADSites", new { p.PadId });
		}
	}
}