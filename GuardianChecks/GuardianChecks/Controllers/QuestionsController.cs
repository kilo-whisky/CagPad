using GuardianChecks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuardianChecks.Controllers
{
	public class QuestionsController : Controller
	{
		// GET: Questions
		public ActionResult Index()
		{
			return View(Questions.GetQuestions(null, null));
		}

		public ActionResult Add()
		{
			Questions q = new Questions();
			return View(q);
		}

		[HttpPost]
		public ActionResult Add(Questions q)
		{
			q.upsert();
			return RedirectToAction("Index", "Questions");
		}

		public ActionResult Edit(int QuestionId)
		{
			Questions q = Questions.GetQuestions(QuestionId, null)[0];
			return View(q);
		}

		[HttpPost]
		public ActionResult Edit(Questions q)
		{
			q.upsert();
			return RedirectToAction("Index", "Questions");
		}
	}
}