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
		public bool Answer { get; set; }

		public int upsert()
		{
			using (dbHelp dbh = new dbHelp("PAD.Answers_Upsert", true, "CAG"))
			{
				dbh.addParam("CheckId", CheckId);
				dbh.addParam("QuestionId", QuestionId);
				dbh.addParam("Answer", Answer);
				dbh.addParam("UserId", 1);
				string retval = dbh.ExecNoQuery();
				return int.Parse(retval);
			}
		}
	}
}