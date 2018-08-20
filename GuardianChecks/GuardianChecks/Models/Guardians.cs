using GuardianChecks.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuardianChecks.Models
{
	public class Guardians
	{
		public int PadId { get; set; }
		public int UserId { get; set; }
		public string AddRemove { get; set; }

		public int upsert()
		{
			using (dbHelp dbh = new dbHelp("PAD.Guardians_Upsert", true, "CAG"))
			{
				dbh.addParam("PadId", PadId);
				dbh.addParam("UserId", UserId);
				dbh.addParam("AddRemove", AddRemove);
				string retval = dbh.ExecNoQuery();
				return int.Parse(retval);
			}
		}
	}

	public class GuardiansSelect
	{
		public int? PadId { get; set; }
		public List<UserModel> UserList { get; set; }
		public List<int> UserSelected { get; set; }
		public GuardiansSelect() { }
		public GuardiansSelect(int? PadId)
		{
			this.PadId = PadId;

			UserList = new List<UserModel>();
			UserSelected = new List<int>();

			using (dbHelp dbh = new dbHelp("Core.User_List", true, "CAG"))
			{
				dbh.addParam("PadId", PadId);
				while (dbh.dr.Read())
				{
					UserModel item = new UserModel();
					item.UserId = dbh.drGetInt32("UserId");
					item.FullName = dbh.drGetString("FullName");
					UserList.Add(item);
				}
				dbh.dr.NextResult();
				while (dbh.dr.Read())
				{
					Guardians item = new Guardians();
					item.UserId = dbh.drGetInt32("UserId");
					UserSelected.Add(item.UserId);
				}
			}
		}
	}
}