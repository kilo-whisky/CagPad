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
			g.Date = DateTime.Now;
			try
			{
				g.upsert();
				return RedirectToAction("Notes", new { PadId = g.PadId, CheckDate = now });
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

	}
}