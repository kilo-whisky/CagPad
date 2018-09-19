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
	[Authorize]
	public class GuardiansController : Controller
	{
		[Authorize(Roles = "SYSADMIN,READER,GUARDIAN")]
		public ActionResult Index()
		{
			return View(GuardianCheck.GetChecks(null, null, null));
		}

		[Authorize(Roles = "SYSADMIN,READER,GUARDIAN")]
		public ActionResult Details(int CheckId)
		{
			GuardianCheck check = GuardianCheck.GetChecks(CheckId, null, null).First();
			ViewBag.PAD = PAD.GetPadSites(check.PadId).First();
			ViewBag.Answers = Answers.GetAnswers(CheckId);
			return View(check);
		}

		[Authorize(Roles = "SYSADMIN,READER")]
		public PartialViewResult _Late()
		{
			return PartialView(GuardianCheck.GetLateChecks(true));
		}

		[Authorize(Roles = "SYSADMIN,GUARDIAN")]
		public PartialViewResult _PadSite(int PadId)
		{
			return PartialView(PAD.GetPadSites(PadId).First());
		}

		[Authorize(Roles = "SYSADMIN,GUARDIAN")]
		public PartialViewResult _KnownIssues(int PadId)
		{
			return PartialView(Issue.GetIssues(null, null, false, PadId, null, null, true));
		}

		[Authorize(Roles = "SYSADMIN,GUARDIAN")]
		public ActionResult Form(int PadId)
		{
			GuardianCheck g = new GuardianCheck();
			g.PadId = PadId;
			ViewBag.PAD = PAD.GetPadSites(PadId);
			return View(g);
		}

		[Authorize(Roles = "SYSADMIN,GUARDIAN")]
		public PartialViewResult _Questions()
		{
			return PartialView(Questions.GetQuestions(null, true));
		}

		[Authorize(Roles = "SYSADMIN,GUARDIAN")]
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
			int issues = Issue.GetIssues(null, CheckId, false, null, null, null, false).Count;
			if (issues > 0) return Url.Action("Issues", "Guardians", new { CheckId });
			else return Url.Action("Confirmation", "Guardians", new { CheckId });
		}

		[Authorize(Roles = "SYSADMIN,GUARDIAN")]
		public ActionResult Issues(int CheckId)
		{
			return View(Issue.GetIssues(null, CheckId, false, null, null, null, true));
		}

		[Authorize(Roles = "SYSADMIN,GUARDIAN")]
		public ActionResult Confirmation (int CheckId)
		{
			ViewBag.Issues = Issue.GetIssues(null, CheckId, false, null, null, null, false).Where(c => c.Severity == 1).Count();
			return View(GuardianCheck.GetChecks(CheckId, null, null).First());
		}

		[Authorize(Roles = "SYSADMIN,GUARDIAN")]
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

		[Authorize(Roles = "SYSADMIN")]
		public PartialViewResult _Guardians(int PadId)
		{
			GuardiansSelect select = new GuardiansSelect(PadId);
			return PartialView(select);
		}

		[Authorize(Roles = "SYSADMIN")]
		public void GuardianUpsert(int PadId, string AddRemove, string UserId)
		{
			Guardians guardian = new Guardians
			{
				PadId = PadId,
				AddRemove = AddRemove,
				UserId = int.Parse(UserId)
			};
			guardian.upsert();
		}
	}
}