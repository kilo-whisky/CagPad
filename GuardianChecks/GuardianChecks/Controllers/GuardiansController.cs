using GuardianChecks.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
			return View(GuardianCheck.GetChecks(null, null, null));
		}

		public ActionResult Details(int CheckId)
		{
			GuardianCheck check = GuardianCheck.GetChecks(CheckId, null, null).First();
			ViewBag.PAD = PAD.GetPadSites(check.PadId).First();
			ViewBag.Answers = Answers.GetAnswers(CheckId);
			return View(check);
		}

		public PartialViewResult _PadSite(int PadId)
		{
			return PartialView(PAD.GetPadSites(PadId).First());
		}

		public PartialViewResult _KnownIssues(int PadId)
		{
			return PartialView(Issue.GetIssues(null, null, false, PadId, null, null));
		}

		public ActionResult Form(int PadId)
		{
			GuardianCheck g = new GuardianCheck();
			g.PadId = PadId;
			ViewBag.PAD = PAD.GetPadSites(PadId);
			return View(g);
		}

		public PartialViewResult _Questions()
		{
			return PartialView(Questions.GetQuestions(null));
		}

		public string Stage1Upsert(string PadId, List<Questions> Questions)
		{
			GuardianCheck gc = new GuardianCheck
			{
				PadId = int.Parse(PadId)
			};
			int CheckId = gc.upsert();
			foreach (var q in Questions)
			{
				Answers a = new Answers
				{
					CheckId = CheckId,
					QuestionId = q.QuestionId,
					Answer = q.Answer
				};
				a.upsert();
			}
			int issues = Issue.GetIssues(null, CheckId, false, null, null, null).Count;
			if (issues > 0) return Url.Action("Issues", "Guardians", new { CheckId });
			else return Url.Action("Confirmation", "Guardians", new { CheckId });
		}

		public ActionResult Issues(int CheckId)
		{
			return View(Issue.GetIssues(null, CheckId, false, null, null, null));
		}

		public ActionResult Confirmation (int CheckId)
		{
			ViewBag.Issues = Issue.GetIssues(null, CheckId, false, null, null, null).Where(c => c.Severity == 1).Count();
			return View(GuardianCheck.GetChecks(CheckId, null, null).First());
		}

		[HttpPost]
		public ActionResult Confirmation (GuardianCheck check)
		{
			check.Complete = true;
			try
			{
				check.upsert();
				return RedirectToAction("Details", "Guardians", new { check.CheckId });
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}