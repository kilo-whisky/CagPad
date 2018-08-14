using GuardianChecks.Helpers;
using System;
using System.Collections.Generic;
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
		public DateTime? WarrantyExpires { get; set; } 
		public DateTime? BatteryExpiry { get; set; }

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
					list.Add(item);
				}
			}
			return list;
		}
	}
}