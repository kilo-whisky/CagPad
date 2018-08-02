using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace GuardianChecks.Helpers
{
	public class CustomRole : RoleProvider
	{
		public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException();
		}

		public override void CreateRole(string roleName)
		{
			throw new NotImplementedException();
		}

		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			throw new NotImplementedException();
		}

		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			throw new NotImplementedException();
		}

		public override string[] GetAllRoles()
		{
			throw new NotImplementedException();
		}

		public override string[] GetRolesForUser(string username)
		{
			List<string> roleList = new List<string>();
			if (HttpContext.Current.Session["roleList"] == null)
			{
				using (dbHelp dbh = new dbHelp("Core.UserRole_list", true, "CAG"))
				{
					dbh.addParam("UserName", username);
					{
						roleList.Add(dbh.drGetString("roleName"));
					}
				}
				HttpContext.Current.Session["roleList"] = roleList;
			}
			else
			{
				roleList = (List<string>)HttpContext.Current.Session["roleList"];
			}
			return roleList.ToArray();
		}

		public override string[] GetUsersInRole(string roleName)
		{
			throw new NotImplementedException();
		}

		public override bool IsUserInRole(string username, string roleName)
		{
			var userRoles = GetRolesForUser(username);
			return userRoles.Contains(roleName);
		}

		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException();
		}

		public override bool RoleExists(string roleName)
		{
			throw new NotImplementedException();
		}
	}
}