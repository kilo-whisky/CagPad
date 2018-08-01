using GuardianChecks.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuardianChecks.Models
{
	public class Issue
	{
		public int IssueId { get; set; }
		public int? PadId { get; set; }
		public int? AnswerId { get; set; }
		public int? CheckId { get; set; }
		public string Question { get; set; }
		public string PadSite { get; set; }
		public int? CabinetId { get; set; }
		public string Cabinet { get; set; }
		public int? DefibId { get; set; }
		public string Defib { get; set; }
		public int? Severity { get; set; }
		public string Description { get; set; }
		public string ReportedBy { get; set; }
		public DateTime ReportedOn { get; set; }
		public bool Resolved { get; set; }
		public string Image { get; set; }

		public int upsert()
		{
			using (dbHelp dbh = new dbHelp("PAD.Issues_Upsert", true, "CAG"))
			{
				dbh.addParam("IssueId", IssueId);
				dbh.addParam("PadId", PadId);
				dbh.addParam("DefibId", DefibId);
				dbh.addParam("CabinetId", CabinetId);
				dbh.addParam("Severity", Severity);
				dbh.addParam("Description", Description);
				dbh.addParam("Image", Image);
				dbh.addParam("UserId", 1);
				dbh.addParam("Resolved", Resolved);
				string retval = dbh.ExecNoQuery();
				return int.Parse(retval);
			}
		}

		public static List<Issue> GetIssues(int? IssueId, int? CheckId, bool? Resolved, int? PadId, int? DefibId, int? CabinetId)
		{
			List<Issue> list = new List<Issue>();
			using (dbHelp dbh = new dbHelp("PAD.Issues_List", true, "CAG"))
			{
				dbh.addParam("IssueId", IssueId);
				dbh.addParam("CheckId", CheckId);
				dbh.addParam("Resolved", Resolved);
				dbh.addParam("PadId", PadId);
				dbh.addParam("DefibId", DefibId);
				dbh.addParam("CabinetId", CabinetId);
				while (dbh.dr.Read())
				{
					Issue item = new Issue();
					item.IssueId = dbh.drGetInt32("IssueId");
					item.PadId = dbh.drGetInt32Null("PadId");
					item.PadSite = dbh.drGetString("PadSite");
					item.AnswerId = dbh.drGetInt32Null("AnswerId");
					item.CheckId = dbh.drGetInt32Null("CheckId");
					item.Question = dbh.drGetString("Question");
					item.DefibId = dbh.drGetInt32Null("DefibId");
					item.Defib = dbh.drGetString("Defib");
					item.CabinetId = dbh.drGetInt32Null("CabinetId");
					item.Cabinet = dbh.drGetString("Cabinet");
					item.Severity = dbh.drGetInt32Null("Severity");
					item.Description = dbh.drGetString("Description");
					item.ReportedBy = dbh.drGetString("ReportedBy");
					item.ReportedOn = dbh.drGetDateTime("ReportedOn");
					item.Resolved = dbh.DrGetBoolean("Resolved");
					item.Image = dbh.drGetString("Image");
					list.Add(item);
				}
			}
			return list;
		}
	}
}