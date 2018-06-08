using GuardianChecks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace GuardianChecks.Controllers
{
	public class GuardiansController : Controller
	{
		// GET: Guardians
		public ActionResult Index()
		{
			return View();
		}

		public PartialViewResult _PadSite(int PadId)
		{
			return PartialView(PAD.GetPadSites(PadId).First());
		}

		public PartialViewResult _KnownIssues(int PadId)
		{
			return PartialView(Issue.GetIssues(null, PadId, null, null).Where(x => !x.Resolved).ToList());
		}

		public ActionResult Form(int PadId)
		{
			GuardianCheck g = new GuardianCheck();
			g.PadId = PadId;
			ViewBag.PAD = PAD.GetPadSites(PadId);
			return View(g);
		}

		public ActionResult Issues(int PadId, DateTime CheckDate)
		{
			ViewBag.Issues = TempData["Issues"];
			TempData["Issues"] = ViewBag.Issues;
			ViewBag.CheckDate = CheckDate;
			ViewBag.PadSite = PAD.GetPadSites(PadId).First();
			return View();
		}

		public ActionResult Notes(int PadId, DateTime CheckDate)
		{
			return View();
		}

		public ActionResult FirstPageSubmit(GuardianCheck g)
		{
			DateTime now = new DateTime();
			List<string> issues = new List<string>();
			if (!g.CabinetOpenLock)
			{
				issues.Add("Does the cabinet open and lock correctly?");
			}
			if (!g.CabinetBatteriesOk)
			{
				issues.Add("Are the lock batteries ok?");
			}
			if (!g.CabinetLightWork)
			{
				issues.Add("Are the cabinet lights working?");
			}
			if (!g.NothingTouchingHeater)
			{
				issues.Add("Have you ensured that nothing is touching the cabinet heater?");
			}
			if (!g.AEDOk)
			{
				issues.Add("Is the AED in operating mode?");
			}
			if (!g.AEDSilent)
			{
				issues.Add("Is the AED silent?");
			}
			if (!g.ResusKit)
			{
				issues.Add("Is the resuscitation kit in the cabinet?");
			}
			if(issues.Count() > 0)
			{
				TempData["Issues"] = issues;
				return RedirectToAction("Issues", new { PadId = g.PadId, CheckDate = now });
			}
			return RedirectToAction("Notes", new { PadId = g.PadId, CheckDate = now });
		}

		
	}
}