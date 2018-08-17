using GuardianChecks.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuardianChecks.Models
{
	public class UserRoles
	{
		public int UserId { get; set; }
		public string RoleName { get; set; }
		public string AddRemove { get; set; }

		public int upsert()
		{
			using (dbHelp dbh = new dbHelp("Core.UserRole_Upsert", true, "CAG"))
			{
				dbh.addParam("UserId", UserId);
				dbh.addParam("RoleName", RoleName);
				dbh.addParam("AddRemove", AddRemove);
				string retval = dbh.ExecNoQuery();
				return int.Parse(retval);
			}
		}

		public static List<UserRoles> GetByUserName(string UserName)
		{
			return GetUserRoles(null, UserName);
		}

		private static List<UserRoles> GetUserRoles(int? UserId, string UserName)
		{
			List<UserRoles> list = new List<UserRoles>();
			using (dbHelp dbh = new dbHelp("Core.UserRole_List", true, "CAG"))
			{
				dbh.addParam("UserId", UserId);
				dbh.addParam("UserName", UserName);
				while (dbh.dr.Read())
				{
					UserRoles item = new UserRoles();
					item.UserId = dbh.drGetInt32("UserId");
					item.RoleName = dbh.drGetString("RoleName");
					list.Add(item);
				}
				return list;
			}
		}
	}

	public class UserRolesSelect
	{
		public int? UserId { get; set; }
		public List<Role> RoleList { get; set; }
		public List<string> RoleSelected { get; set; }
		public UserRolesSelect() { }
		public UserRolesSelect(int? UserId)
		{
			this.UserId = UserId;

			RoleList = new List<Role>();
			RoleSelected = new List<string>();

			using (dbHelp dbh = new dbHelp("Core.Role_List", true, "CAG"))
			{
				dbh.addParam("UserId", UserId);
				while (dbh.dr.Read())
				{
					Role item = new Role();
					item.RoleName = dbh.drGetString("RoleName");
					item.RoleDescription = dbh.drGetString("RoleDescription");
					RoleList.Add(item);
				}
				dbh.dr.NextResult();
				while (dbh.dr.Read())
				{
					UserRoles item = new UserRoles();
					item.RoleName = dbh.drGetString("RoleName");
					RoleSelected.Add(item.RoleName);
				}
			}
		}
	}
}