using GuardianChecks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuardianChecks.Controllers
{
	public class IssueNotesController : Controller
	{
		// GET: IssueNotes
		public PartialViewResult _Notes(int IssueId)
		{
			return PartialView(IssueNote.GetNotes(IssueId, null));
		}

		public ActionResult Add(int IssueId)
		{
			IssueNote note = new IssueNote();
			return View(note);
		}

		[HttpPost]
		public ActionResult Add(IssueNote n)
		{
			n.upsert();
			return RedirectToAction("Details", "Issues", new { n.IssueId });
		}

		public ActionResult Trash(int IssueId, int NoteId)
		{
			IssueNote n = new IssueNote()
			{
				IssueId = IssueId,
				NoteId = NoteId
			};
			n.upsert();
			return RedirectToAction("Details", "Issues", new { n.IssueId });
		}
	}
}