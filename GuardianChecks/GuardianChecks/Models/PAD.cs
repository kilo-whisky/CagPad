using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuardianChecks.Helpers;

namespace GuardianChecks.Models
{
	public class PAD
	{
		public int PadId { get; set; }
		[DisplayName("Location")]
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
		[AllowHtml]
		public string Map { get; set; }

		public int upsert()
		{
			using (dbHelp dbh = new dbHelp("PAD.PadSite_Upsert", true, "CAG"))
			{
				dbh.addParam("Location", LocationName);
				dbh.addParam("Address", Address);
				dbh.addParam("Description", Description);
				dbh.addParam("Cabinet", CabinetId);
				dbh.addParam("Defib", DefibId);
				dbh.addParam("Owner", Owner);
				dbh.addParam("OwnerTel", OwnerTel);
				dbh.addParam("OwnerEmail", OwnerEmail);
				dbh.addParam("InstallDate", InstallDate);
				dbh.addParam("Funding", Funding);
				dbh.addParam("Amount", Amount);
				dbh.addParam("Insurance", Insurance);
				dbh.addParam("Map", Map);
				string retval = dbh.ExecNoQuery();
				return int.Parse(retval);
			}
		}

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
					item.Map = dbh.drGetString("Map");
					list.Add(item);
				}
			}
			return list;
		}
	}
}