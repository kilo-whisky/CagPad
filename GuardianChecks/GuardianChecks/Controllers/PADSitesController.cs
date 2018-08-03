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
	}
}