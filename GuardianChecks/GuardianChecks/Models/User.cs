using GuardianChecks.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace GuardianChecks.Models
{
	public class User
	{
		public int UserId { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string EmailAddress { get; set; }
		public string Telephone { get; set; }
		public string FullName { get; set; }
		public bool Active { get; set; }
		public virtual ICollection<Role> Roles { get; set; }

		public upsert()
		{
			using (dbHelp dbh = new dbHelp("Core.User_Upsert", true, "CAG"))
			{
				dbh.addParam("UserId", UserId);
				dbh.addParam("UserName", UserName);
				dbh.addParam("EmailAddress", EmailAddress);
			}

		public static User GetByEmailAddress (string EmailAddress)
		{
			return GetUserList(null, null, EmailAddress)[0];
		}

		public static User GetByUsername(string UserName)
		{
			return GetUserList(null, UserName, null)[0];
		}

		public static List<User> GetUsers()
		{
			return GetUserList(null, null, null);
		}

		private static List<User> GetUserList(int? UserId, string UserName, string EmailAddress)
		{
			List<User> list = new List<User>();
			using (dbHelp dbh = new dbHelp("Core.User_List", true, "CAG"))
			{
				dbh.addParam("UserId", UserId);
				dbh.addParam("UserName", UserName);
				dbh.addParam("EmailAddress", EmailAddress);
				while (dbh.dr.Read())
				{
					User item = new User();
					item.UserId = dbh.drGetInt32("UserId");
					item.UserName = dbh.drGetString("UserName");
					item.Password = dbh.drGetString("Password");
					item.FirstName = dbh.drGetString("FirstName");
					item.LastName = dbh.drGetString("LastName");
					item.EmailAddress = dbh.drGetString("EmailAddress");
					item.Telephone = dbh.drGetString("Telephone");
					item.FullName = dbh.drGetString("FullName");
					list.Add(item);
				}
				return list;
			}
		}
	}

	public class CustomMembershipUser : MembershipUser
	{
		public int UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public ICollection<Role> Roles { get; set; }


		public CustomMembershipUser(User user):base("CustomMembership", user.UserName, user.UserId, user.EmailAddress, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
			{
			UserId = user.UserId;
			FirstName = user.FirstName;
			LastName = user.LastName;
			Roles = user.Roles;
			}
	}

}