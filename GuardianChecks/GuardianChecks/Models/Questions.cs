using GuardianChecks.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuardianChecks.Models
{
	public class Questions
	{
		public int QuestionId { get; set; }
		public string Question { get; set; }
		public string Type { get; set; }
		public string TypeName { get; set; }
		public bool Active { get; set; }
		public bool Answer { get; set; }
		public int? QuestionOrder { get; set; }

		public int upsert()
		{
			using (dbHelp dbh = new dbHelp("PAD.Questions_Upsert", true, "CAG"))
			{
				dbh.addParam("QuestionId", QuestionId);
				dbh.addParam("Question", Question);
				dbh.addParam("Type", Type);
				dbh.addParam("Active", Active);
				dbh.addParam("QuestionOrder", QuestionOrder);
				string retval = dbh.ExecNoQuery();
				return int.Parse(retval);
			}
		}

		public static List<Questions> GetQuestions(int? QuestionId, bool? Active)
		{
			List<Questions> list = new List<Questions>();
			using (dbHelp dbh = new dbHelp("PAD.Questions_List", true, "CAG"))
			{
				dbh.addParam("QuestionId", QuestionId);
				dbh.addParam("Active", Active);
				while (dbh.dr.Read())
				{
					Questions item = new Questions();
					item.QuestionId = dbh.drGetInt32("QuestionId");
					item.Question = dbh.drGetString("Question");
					item.Type = dbh.drGetString("Type");
					item.TypeName = dbh.drGetString("TypeName");
					item.Active = dbh.DrGetBoolean("Active");
					item.QuestionOrder = dbh.drGetInt32Null("QuestionOrder");
					list.Add(item);
				}
			}
			return list;
		}
	}
}