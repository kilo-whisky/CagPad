using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuardianChecks.Models;
using Newtonsoft.Json;

namespace GuardianChecks.Controllers
{
	[Authorize]
	public class IssuesController : Controller
	{
		// GET: Issues
		public ActionResult Index()
		{
			return View(Issue.GetIssues(null, null, false, null, null, null));
		}

		public ActionResult Details(int IssueId)
		{
			return View(Issue.GetIssues(IssueId, null, null, null, null, null).First());
		}

		[HttpPost]
		public ActionResult IssueFormUpsert(HttpPostedFileBase image)
		{
			var input = Request.Form["issue"];
			Issue issue = JsonConvert.DeserializeObject<Issue>(input);
			Issue existing = Issue.GetIssues(issue.IssueId, null, null, null, null, null).First();

			if (image != null && image.ContentLength > 0)
			{
				var ext = Path.GetExtension(image.FileName);
				var path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Uploads/"), issue.IssueId + ext);
				var logical = "/Uploads/" + issue.IssueId + ext;
				image.SaveAs(path);
				existing.Image = logical;
			}

			existing.Description = issue.Description;
			existing.Severity = issue.Severity;

			existing.upsert();

			return Json("done", JsonRequestBehavior.AllowGet);
		}
	}
}