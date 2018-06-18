using GuardianChecks.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuardianChecks.Models
{
	public class GuardianIssues
	{
		public string IssueName { get; set; }
		public string IssueQuestion { get; set; }


		public static List<GuardianIssues> GetIssues(int? PadId, DateTime? Date)
		{
			List<GuardianIssues> list = new List<GuardianIssues>();
			using (dbHelp dbh = new dbHelp("PAD.GuardianCheck_Issues", true, "CAG"))
			{
				dbh.addParam("PadId", PadId);
				dbh.addParam("Date", Date);
				while (dbh.dr.Read())
				{
					GuardianIssues item = new GuardianIssues();
					item.IssueName = dbh.drGetString("IssueName");
					item.IssueQuestion = dbh.drGetString("IssueQuestion");
					list.Add(item);
				}
			}
			return list;
		}
	}

	public class GuardianCheck
	{
		public int PadId { get; set; }
		public DateTime Date { get; set; }
		public int UserId { get; set; }
		public string Guardian { get; set; }
		public bool CabinetOpenLock { get; set; }
		public bool CabinetBatteriesOk { get; set; }
		public bool CabinetLightWork { get; set; }
		public bool NothingTouchingHeater { get; set; }
		public bool AEDOk { get; set; }
		public bool AEDSilent { get; set; }
		public List<string> Issues { get; set; }

		public int upsert()
		{
			using (dbHelp dbh = new dbHelp("PAD.GuardianCheck_Upsert", true, "CAG"))
			{
				dbh.addParam("PadId", PadId);
				dbh.addParam("Date", Date);
				dbh.addParam("UserId", 1);
				dbh.addParam("CabinetOpenLock", CabinetOpenLock);
				dbh.addParam("CabinetBatteriesOk", CabinetBatteriesOk);
				dbh.addParam("CabinetLightWork", CabinetLightWork);
				dbh.addParam("NothingTouchingHeater", NothingTouchingHeater);
				dbh.addParam("AEDOk", AEDOk);
				dbh.addParam("AEDSilent", AEDSilent);
				string retval = dbh.ExecNoQuery();
				return int.Parse(retval);
			}
		}

		public static List<GuardianCheck> GetChecks(int? PadId, DateTime? Date)
		{
			List<GuardianCheck> list = new List<GuardianCheck>();
			using (dbHelp dbh = new dbHelp("PAD.GuardianCheck_List", true, "CAG"))
			{
				dbh.addParam("PadId", PadId);
				dbh.addParam("Date", Date);
				while (dbh.dr.Read())
				{
					GuardianCheck item = new GuardianCheck();
					item.PadId = dbh.drGetInt32("PadId");
					item.Date = dbh.drGetDateTime("Date");
					item.UserId = dbh.drGetInt32("UserId");
					item.Guardian = dbh.drGetString("Guardian");
					item.CabinetOpenLock = dbh.DrGetBoolean("CabinetOpenLock");
					item.CabinetBatteriesOk = dbh.DrGetBoolean("CabinetBatteriesOk");
					item.CabinetLightWork = dbh.DrGetBoolean("CabinetLightWork");
					item.NothingTouchingHeater = dbh.DrGetBoolean("NothingTouchingHeater");
					item.AEDOk = dbh.DrGetBoolean("AEDOk");
					item.AEDSilent = dbh.DrGetBoolean("AEDSilent");
					list.Add(item);
				}
			}
			return list;
		}

	}
}