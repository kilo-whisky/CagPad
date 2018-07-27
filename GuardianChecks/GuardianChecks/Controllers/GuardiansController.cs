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
			return View();
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

		public ActionResult Stage1 (int PadId, List<Questions> Questions)
		{
			GuardianCheck gc = new GuardianCheck
			{
				PadId = PadId
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
		}
	}
}