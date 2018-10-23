using GuardianChecks.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuardianChecks.Models
{
	public class QuestionSets
	{
		public int? SetId { get; set; }
		public int? QuestionId { get; set; }
		public string Name { get; set; }
		public string AddRemove { get; set; }

		public int upsert()
		{
			using (dbHelp dbh = new dbHelp("PAD.QuestionSet_Upsert", true, "CAG"))
			{
				dbh.addParam("SetId", SetId);
				dbh.addParam("Name", Name);
				string retval = dbh.ExecNoQuery();
				return int.Parse(retval);
			}
		}

		public static List<QuestionSets> GetSets(int? SetId)
		{
			List<QuestionSets> list = new List<QuestionSets>();
			using (dbHelp dbh = new dbHelp("PAD.QuestionSet_List", true, "CAG"))
			{
				dbh.addParam("SetId", SetId);
				while (dbh.dr.Read())
				{
					QuestionSets item = new QuestionSets();
					item.SetId = dbh.drGetInt32("SetId");
					item.Name = dbh.drGetString("Name");
					list.Add(item);
				}
			}
			return list;
		}

		public static int SetQuestions(int SetId, int QuestionId, string AddRemove)
		{
			using (dbHelp dbh = new dbHelp("PAD.SetQuestions_Upsert", true, "CAG"))
			{
				dbh.addParam("SetId", SetId);
				dbh.addParam("QuestionId", QuestionId);
				dbh.addParam("AddRemove", AddRemove);
				string retval = dbh.ExecNoQuery();
				return int.Parse(retval);
			}
		}
	}
}