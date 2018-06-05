using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GuardianChecks.Helpers;

namespace GuardianChecks.Models
{
	public class PAD
	{
		public int PadId { get; set; }
		public string LocationName { get; set; }
		public string Address { get; set; }
		public string Description { get; set; }
		public int CabinetId { get; set; }
		public string Cabinet { get; set; }
		public int DefibId { get; set; }
		public string Defib { get; set; }
		public string Owner { get; set; }
		public string OwnerTel { get; set; }
		public string OwnerEmail { get; set; }
		public DateTime? InstallDate { get; set; }
		public string Funding { get; set; }
		public decimal? Amount { get; set; }
		public string Insurance { get; set; }

		public static List<PAD> GetPadSites(int? PadId)
		{
			List<PAD> list = new List<PAD>();
			using (dbHelp dbh = new dbHelp("PAD.PadSite_List", true, "CAG"))
			{
				dbh.addParam("PadId", PadId);
				while (dbh.dr.Read())
				{
					PAD item = new PAD();
					item.PadId = dbh.drGetInt32("PadId");
					item.LocationName = dbh.drGetString("Location");
					item.Address = dbh.drGetString("Address");
					item.Description = dbh.drGetString("Description");
					item.CabinetId = dbh.drGetInt32("CabinetId");
					item.Cabinet = dbh.drGetString("Cabinet");
					item.DefibId = dbh.drGetInt32("DefibId");
					item.Defib = dbh.drGetString("Defib");
					item.Owner = dbh.drGetString("Owner");
					item.OwnerTel = dbh.drGetString("OwnerTel");
					item.OwnerEmail = dbh.drGetString("OwnerEmail");
					item.InstallDate = dbh.drGetDateTimeNull("InstallDate");
					item.Funding = dbh.drGetString("Funding");
					item.Amount = dbh.drGetDecimalNull("Amount");
					item.Insurance = dbh.drGetString("Insurance");
					list.Add(item);
				}
			}
			return list;
		}
	}
}