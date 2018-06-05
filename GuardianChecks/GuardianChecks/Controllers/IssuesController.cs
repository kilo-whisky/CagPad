using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuardianChecks.Models;

namespace GuardianChecks.Controllers
{
	public class IssuesController : Controller
	{
		// GET: Issues
		public ActionResult Index()
		{
			return View(Issue.GetIssues(null, null, null, null));
		}

		public ActionResult Details(int IssueId)
		{
			return View(Issue.GetIssues(IssueId, null, null, null).First());
		}
	}
}