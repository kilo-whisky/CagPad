using GuardianChecks.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuardianChecks.Models
{
	
	public class GuardianCheck
	{
		public int? CheckId { get; set; }
		public int PadId { get; set; }
		public string Location { get; set; }
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime? Date { get; set; }
		public int UserId { get; set; }
		public bool Complete { get; set; }
		public DateTime? Completed { get; set; }
		public string Guardian { get; set; }
		public string Notes { get; set; }
		public int Issues { get; set; }
		public DateTime? LastCheck { get; set; }
		public bool Late { get; set; }
		

		public int upsert()
		{
			using (dbHelp dbh = new dbHelp("PAD.GuardianCheck_Upsert", true, "CAG"))
			{
				dbh.addParam("CheckId", CheckId);
				dbh.addParam("PadId", PadId);
				dbh.addParam("Date", Date);
				dbh.addParam("UserId", (int)HttpContext.Current.Session["UserId"]);
				dbh.addParam("Complete", Complete);
				dbh.addParam("Notes", Notes);
				string retval = dbh.ExecNoQuery();
				return int.Parse(retval);
			}
		}

		public static List<GuardianCheck> GetLateChecks(bool? Late)
		{
			List<GuardianCheck> list = new List<GuardianCheck>();
			using (dbHelp dbh = new dbHelp("PAD.GuardianCheck_Late", true, "CAG"))
			{
				dbh.addParam("Late", Late);
				while (dbh.dr.Read())
				{
					GuardianCheck item = new GuardianCheck();
					item.PadId = dbh.drGetInt32("PadId");
					item.Location = dbh.drGetString("Location");
					item.LastCheck = dbh.drGetDateTimeNull("LastCheck");
					item.Late = dbh.DrGetBoolean("Late");
					list.Add(item);
				}
			}
			return list;
		}

		public static List<GuardianCheck> GetChecks(int? CheckId, int? PadId, DateTime? Date)
		{
			List<GuardianCheck> list = new List<GuardianCheck>();
			using (dbHelp dbh = new dbHelp("PAD.GuardianCheck_List", true, "CAG"))
			{
				dbh.addParam("CheckId", CheckId);
				dbh.addParam("PadId", PadId);
				dbh.addParam("Date", Date);
				while (dbh.dr.Read())
				{
					GuardianCheck item = new GuardianCheck();
					item.CheckId = dbh.drGetInt32("CheckId");
					item.PadId = dbh.drGetInt32("PadId");
					item.Location = dbh.drGetString("Location");
					item.Date = dbh.drGetDateTime("Date");
					item.Completed = dbh.drGetDateTimeNull("Completed");
					item.UserId = dbh.drGetInt32("UserId");
					item.Guardian = dbh.drGetString("Guardian");
					item.Issues = dbh.drGetInt32("Issues");
					item.Notes = dbh.drGetString("Notes");
					list.Add(item);
				}
			}
			return list;
		}

	}
}