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
		public bool Active { get; set; }
		public int? QuestionOrder { get; set; }

		public static List<Questions> GetQuestions(int? QuestionId)
		{
			List<Questions> list = new List<Questions>();
			using (dbHelp dbh = new dbHelp("PAD.Questions_List", true, "CAG"))
			{
				dbh.addParam("QuestionId", QuestionId);
				while (dbh.dr.Read())
				{
					Questions item = new Questions();
					item.QuestionId = dbh.drGetInt32("QuestionId");
					item.Question = dbh.drGetString("Question");
					item.Type = dbh.drGetString("Type");
					item.Active = dbh.DrGetBoolean("Active");
					item.QuestionOrder = dbh.drGetInt32Null("QuestionOrder");
					list.Add(item);
				}
			}
			return list;
		}
	}
}