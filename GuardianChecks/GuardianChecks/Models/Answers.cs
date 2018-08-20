using GuardianChecks.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuardianChecks.Models
{
	public class Answers
	{
		public int CheckId { get; set; }
		public int QuestionId { get; set; }
		public string Question { get; set; }
		public int AnswerId { get; set; }
		public bool Answer { get; set; }
		public int? IssueId { get; set; }
		public string Description { get; set; }
		public int? Severity { get; set; }
		public string Image { get; set; }
		public bool? Resolved { get; set; }

		public int upsert()
		{
			using (dbHelp dbh = new dbHelp("PAD.Answers_Upsert", true, "CAG"))
			{
				dbh.addParam("CheckId", CheckId);
				dbh.addParam("QuestionId", QuestionId);
				dbh.addParam("Answer", Answer);
				dbh.addParam("UserId", (int)HttpContext.Current.Session["UserId"]);
				string retval = dbh.ExecNoQuery();
				return int.Parse(retval);
			}
		}

		public static List<Answers> GetAnswers(int? CheckId)
		{
			List<Answers> list = new List<Answers>();
			using (dbHelp dbh = new dbHelp("PAD.Answers_List", true, "CAG"))
			{
				dbh.addParam("CheckId", CheckId);
				while (dbh.dr.Read())
				{
					Answers item = new Answers();
					item.CheckId = dbh.drGetInt32("CheckId");
					item.QuestionId = dbh.drGetInt32("QuestionId");
					item.Question = dbh.drGetString("Question");
					item.AnswerId = dbh.drGetInt32("AnswerId");
					item.Answer = dbh.DrGetBoolean("Answer");
					item.IssueId = dbh.drGetInt32Null("IssueId");
					item.Description = dbh.drGetString("Description");
					item.Severity = dbh.drGetInt32Null("Severity");
					item.Image = dbh.drGetString("Image");
					item.Resolved = dbh.DrGetBooleanNull("Resolved");
					list.Add(item);
				}
			}
			return list;
		}
	}
}