﻿using GuardianChecks.Models;
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
		public ActionResult Index()
		{
			return View(PAD.GetPadSites(null));
		}

		public ActionResult Site(int PadId)
		{
			return View(PAD.GetPadSites(PadId).First());
		}

		public PartialViewResult _Equipment(int PadId)
		{
			return PartialView(Equipment.GetEquipment(PadId));
		}

		public PartialViewResult _Checks(int PadId)
		{
			return PartialView(GuardianCheck.GetChecks(null, PadId, null));
		}

		public PartialViewResult _Guardians (int PadId)
		{
			return PartialView();
		}

		public ActionResult Create()
		{
			PAD p = new PAD();
			ViewBag.Cabinets = Cabinet.GetCabinets(null, false);
			ViewBag.Defibs = Defib.GetDefibs(null, false);
			return View(p);
		}

		[HttpPost]
		public ActionResult Create(PAD p)
		{
			int PadId = p.upsert();
			return RedirectToAction("Site", "PADSites", new { PadId });
		}

		public ActionResult Edit(int PadId)
		{
			ViewBag.Cabinets = Cabinet.GetCabinets(null, false);
			ViewBag.Defibs = Defib.GetDefibs(null, false);
			return View(PAD.GetPadSites(PadId)[0]);
		}

		[HttpPost]
		public ActionResult Edit(PAD p)
		{
			p.upsert();
			return RedirectToAction("Site", "PADSites", new { p.PadId });
		}
	}
}