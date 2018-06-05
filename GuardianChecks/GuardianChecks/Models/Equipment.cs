using GuardianChecks.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuardianChecks.Models
{
	public class Equipment
	{
		public int? PadId { get; set; }
		public int? CabinetId { get; set; }
		public int? DefibId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Supplier { get; set; }
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime? Expiry { get; set; }
		public int MinorIssues { get; set; }
		public int MajorIssues { get; set; }

		public static List<Equipment> GetEquipment(int? PadId)
		{
			List<Equipment> list = new List<Equipment>();
			using (dbHelp dbh = new dbHelp("PAD.Equipment_List", true, "CAG"))
			{
				dbh.addParam("PadId", PadId);
				while (dbh.dr.Read())
				{
					Equipment item = new Equipment();
					item.PadId = dbh.drGetInt32Null("PadId");
					item.CabinetId = dbh.drGetInt32Null("CabinetId");
					item.DefibId = dbh.drGetInt32Null("DefibId");
					item.Name = dbh.drGetString("Name");
					item.Description = dbh.drGetString("Description");
					item.Supplier = dbh.drGetString("Supplier");
					item.Expiry = dbh.drGetDateTimeNull("Expiry");
					item.MinorIssues = dbh.drGetInt32("MinorIssues");
					item.MajorIssues = dbh.drGetInt32("MajorIssues");
					list.Add(item);
				}
			}
			return list;
		}
	}
}