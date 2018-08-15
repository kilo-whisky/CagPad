using GuardianChecks.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
		[DisplayName("Heartsafe Number")]
		public int? HeartSafeNumber { get; set; }
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime? Expiry { get; set; }
		public int? PadId { get; set; }
		public string Location { get; set; }

		public int upsert()
		{
			using (dbHelp dbh = new dbHelp("PAD.Cabinet_Upsert", true, "CAG"))
			{
				dbh.addParam("CabinetId", CabinetId);
				dbh.addParam("Name", Name);
				dbh.addParam("Description", Description);
				dbh.addParam("UserId", (int)HttpContext.Current.Session["UserId"]);
				dbh.addParam("Supplier", Supplier);
				dbh.addParam("HeartSafeNumber", HeartSafeNumber);
				dbh.addParam("Expiry", Expiry);
				string retval = dbh.ExecNoQuery();
				return int.Parse(retval);
			}
		}

		public static List<Cabinet> GetCabinets(int? CabinetId, bool? Selected)
		{
			List<Cabinet> list = new List<Cabinet>();
			using (dbHelp dbh = new dbHelp("PAD.Cabinet_List", true, "CAG"))
			{
				dbh.addParam("CabinetId", CabinetId);
				dbh.addParam("Selected", Selected);
				while (dbh.dr.Read())
				{
					Cabinet item = new Cabinet();
					item.CabinetId = dbh.drGetInt32("CabinetId");
					item.Name = dbh.drGetString("Name");
					item.Description = dbh.drGetString("Description");
					item.Supplier = dbh.drGetString("Supplier");
					item.HeartSafeNumber = dbh.drGetInt32Null("HeartSafeNumber");
					item.Expiry = dbh.drGetDateTimeNull("WarrantyExpiry");
					item.PadId = dbh.drGetInt32Null("PadId");
					item.Location = dbh.drGetString("Location");
					list.Add(item);
				}
			}
			return list;
		}
	}
}