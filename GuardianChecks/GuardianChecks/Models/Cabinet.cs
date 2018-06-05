using GuardianChecks.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuardianChecks.Models
{
	public class Cabinet
	{
		public int CabinetId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Supplier { get; set; }
		public int? HeartSafeNumber { get; set; }
		public DateTime? Expiry { get; set; }

		public static List<Cabinet> GetCabinets(int? CabinetId)
		{
			List<Cabinet> list = new List<Cabinet>();
			using (dbHelp dbh = new dbHelp("PAD.Cabinet_List", true, "CAG"))
			{
				dbh.addParam("CabinetId", CabinetId);
				while (dbh.dr.Read())
				{
					Cabinet item = new Cabinet();
					item.CabinetId = dbh.drGetInt32("CabinetId");
					item.Name = dbh.drGetString("Name");
					item.Description = dbh.drGetString("Description");
					item.Supplier = dbh.drGetString("Supplier");
					item.HeartSafeNumber = dbh.drGetInt32Null("HeartSafeNumber");
					item.Expiry = dbh.drGetDateTimeNull("WarrantyExpiry");
					list.Add(item);
				}
			}
			return list;
		}
	}
}