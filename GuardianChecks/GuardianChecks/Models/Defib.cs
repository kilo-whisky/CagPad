using GuardianChecks.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuardianChecks.Models
{
	public class Defib
	{
		public int DefibId { get; set; } 
		public string Name { get; set; } 
		public string Description { get; set; } 
		public string Supplier { get; set; } 
		public string Serial { get; set; } 
		[DisplayName("Warranty Expires")]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime? WarrantyExpires { get; set; }
		[DisplayName("Battery Expires")]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime? BatteryExpiry { get; set; }
		public int? PadId { get; set; }
		public string Location { get; set; }

		public int upsert()
		{
			using (dbHelp dbh = new dbHelp("PAD.Defib_Upsert", true, "CAG"))
			{
				dbh.addParam("DefibId", DefibId);
				dbh.addParam("Name", Name);
				dbh.addParam("Description", Description);
				dbh.addParam("UserId", (int)HttpContext.Current.Session["UserId"]);
				dbh.addParam("Supplier", Supplier);
				dbh.addParam("Serial", Serial);
				dbh.addParam("WarrantyExpires", WarrantyExpires);
				dbh.addParam("BatteryExpiry", BatteryExpiry);
				string retval = dbh.ExecNoQuery();
				return int.Parse(retval);
			}
		}

		public static List<Defib> GetDefibs(int? DefibId, bool? Selected)
		{
			List<Defib> list = new List<Defib>();
			using (dbHelp dbh = new dbHelp("PAD.Defib_List", true, "CAG"))
			{
				dbh.addParam("DefibId", DefibId);
				dbh.addParam("Selected", Selected);
				while (dbh.dr.Read())
				{
					Defib item = new Defib();
					item.DefibId = dbh.drGetInt32("DefibId");
					item.Name = dbh.drGetString("Name");
					item.Description = dbh.drGetString("Description");
					item.Supplier = dbh.drGetString("Supplier");
					item.Serial = dbh.drGetString("Serial");
					item.WarrantyExpires = dbh.drGetDateTimeNull("WarrantyExpires");
					item.BatteryExpiry = dbh.drGetDateTimeNull("BatteryExpiry");
					item.PadId = dbh.drGetInt32Null("PadId");
					item.Location = dbh.drGetString("Location");
					list.Add(item);
				}
			}
			return list;
		}
	}
}