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
		[Authorize(Roles = "SYSADMIN,READER")]
		public ActionResult Index()
		{
			return View();
		}

		[Authorize(Roles = "SYSADMIN,READER")]
		public PartialViewResult _Unresolved()
		{
			return PartialView(Issue.GetIssues(null, null, false, null, null, null, true).Where(x => x.Severity != null).ToList());
		}

		[Authorize(Roles = "SYSADMIN,READER")]
		public PartialViewResult _Resolved()
		{
			return PartialView(Issue.GetIssues(null, null, true, null, null, null, null));
		}

		[Authorize(Roles = "SYSADMIN,READER")]
		public ActionResult Details(int IssueId)
		{
			return View(Issue.GetIssues(IssueId, null, null, null, null, null, null).First());
		}

		[Authorize(Roles = "SYSADMIN,READER")]
		public ActionResult ResolvedIssue(int IssueId)
		{
			return View(Issue.GetIssues(IssueId, null, null, null, null, null, null).First());
		}

		[Authorize(Roles = "SYSADMIN")]
		public ActionResult ChangeSeverity(int IssueId, int Severity)
		{
			Issue i = Issue.GetIssues(IssueId, null, null, null, null, null, null)[0];
			i.Severity = Severity;
			i.upsert();
			string note = "";
			if (Severity == 1) note = "Changed severity of this issue to 'Severe'";
			if (Severity == 2) note = "Changed severity of this issue to 'Warning'";
			if (Severity == 3) note = "Changed severity of this issue to 'Severe'";
			IssueNote n = new IssueNote()
			{
				IssueId = i.IssueId,
				Note = note
			};
			n.upsert();
			return RedirectToAction("Details", "Issues", new { IssueId });
		}

		[Authorize(Roles = "SYSADMIN")]
		public JsonResult Resolve(int IssueId, bool Resolved)
		{
			Issue i = Issue.GetIssues(IssueId, null, null, null, null, null, null)[0];
			i.Resolved = true;
			i.upsert();
			string note = "";
			if (Resolved) note = "Issue marked as resolved";
			if (!Resolved) note = "Issue marked as unresolved and re-opened";
			IssueNote n = new IssueNote()
			{
				IssueId = i.IssueId,
				Note = note
			};
			n.upsert();
			if (Resolved)
			{
				return Json(Url.Action("Index", "Issues"), JsonRequestBehavior.AllowGet);
			}
			else
			{
				return Json(Url.Action("Details", "Issues", new { i.IssueId }), JsonRequestBehavior.AllowGet);
			}
		}

		[Authorize(Roles = "SYSADMIN,READER,GUARDIAN")]
		[HttpPost]
		public ActionResult IssueFormUpsert(HttpPostedFileBase image)
		{
			var input = Request.Form["issue"];
			Issue issue = JsonConvert.DeserializeObject<Issue>(input);
			Issue existing = Issue.GetIssues(issue.IssueId, null, null, null, null, null, null).First();

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