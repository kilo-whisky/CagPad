using GuardianChecks.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuardianChecks.Models
{
	public class Role
	{
		public string RoleName { get; set; }
		public string RoleDescription { get; set; }
		
	}

	public class UserRoles
	{
		public int UserId { get; set; }
		public string RoleName { get; set; }

		public static List<UserRoles> GetByUserName(string UserName)
		{
			return GetUserRoles(null, UserName);
		}

		private static List<UserRoles> GetUserRoles (int? UserId, string UserName)
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
}