using GuardianChecks.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GuardianChecks.Models
{
	public class IssueNote
	{
		[DisplayName("Issue")]
		public int? IssueId { get; set; }
		public int? NoteId { get; set; }
		[DisplayName("Note By")]
		public string NoteBy { get; set; }
		[DisplayName("Note On")]
		public DateTime NoteOn { get; set; }
		public string Note { get; set; }

		public int upsert()
		{
			using (dbHelp dbh = new dbHelp("PAD.IssueNotes_Upsert", true, "CAG"))
			{
				dbh.addParam("IssueId", IssueId);
				dbh.addParam("NoteId", NoteId);
				dbh.addParam("UserId", (int)HttpContext.Current.Session["UserId"]);
				dbh.addParam("Note", Note);
				string retval = dbh.ExecNoQuery();
				return int.Parse(retval);
			}
		}

		public static List<IssueNote> GetNotes(int IssueId, int? NoteId)
		{
			List<IssueNote> list = new List<IssueNote>();
			using (dbHelp dbh = new dbHelp("PAD.IssueNotes_List", true, "CAG"))
			{
				dbh.addParam("IssueId", IssueId);
				dbh.addParam("NoteId", NoteId);
				while (dbh.dr.Read())
				{
					IssueNote item = new IssueNote();
					item.IssueId = dbh.drGetInt32("IssueId");
					item.NoteId = dbh.drGetInt32("NoteId");
					item.NoteBy = dbh.drGetString("NoteBy");
					item.NoteOn = dbh.drGetDateTime("NoteOn");
					item.Note = dbh.drGetString("Note");
					list.Add(item);
				}
			}
			return list;
		}
	}
}