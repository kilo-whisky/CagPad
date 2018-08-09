using GuardianChecks.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;
using static GuardianChecks.Helpers.Authentication;

namespace GuardianChecks.Models
{
	public class PasswordReset
	{
		public int UserId { get; set; }
		public string CurrentPassword { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		[Compare("Password", ErrorMessage = "Passwords do not match, try again!")]
		public string ComparePassword { get; set; }
	}

	public class UserModel
	{
		public int UserId { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Salt { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string EmailAddress { get; set; }
		public string Telephone { get; set; }
		public string FullName { get; set; }
		public bool Active { get; set; }
		public virtual ICollection<Role> Roles { get; set; }

		public int upsert()
		{
			//LoginInfo hash = PasswordHash(UserName, Password);
			using (dbHelp dbh = new dbHelp("Core.User_Upsert", true, "CAG"))
			{
				dbh.addParam("UserId", UserId);
				dbh.addParam("UserName", UserName);
				if (Salt != null || Password != null)
				{ 
					dbh.addParam("Password", PasswordHash(Password, Salt));
					dbh.addParam("Salt", Salt);
				}
				dbh.addParam("FirstName", FirstName);
				dbh.addParam("LastName", LastName);
				dbh.addParam("EmailAddress", EmailAddress);
				dbh.addParam("Telephone", Telephone);
				dbh.addParam("Active", Active);
				string retval = dbh.ExecNoQuery();
				return int.Parse(retval);
			}
		}

		public static UserModel GetByUserId (int UserId)
		{
			return GetUserList(UserId, null, null)[0];
		}

		public static UserModel GetByEmailAddress (string EmailAddress)
		{
			return GetUserList(null, null, EmailAddress)[0];
		}

		public static UserModel GetByUsername(string UserName)
		{
			return GetUserList(null, UserName, null)[0];
		}

		public static List<UserModel> GetUsers()
		{
			return GetUserList(null, null, null);
		}

		private static List<UserModel> GetUserList(int? UserId, string UserName, string EmailAddress)
		{
			List<UserModel> list = new List<UserModel>();
			using (dbHelp dbh = new dbHelp("Core.User_List", true, "CAG"))
			{
				dbh.addParam("UserId", UserId);
				dbh.addParam("UserName", UserName);
				dbh.addParam("EmailAddress", EmailAddress);
				while (dbh.dr.Read())
				{
					UserModel item = new UserModel();
					item.UserId = dbh.drGetInt32("UserId");
					item.UserName = dbh.drGetString("UserName");
					item.Password = dbh.drGetString("Password");
					item.FirstName = dbh.drGetString("FirstName");
					item.LastName = dbh.drGetString("LastName");
					item.EmailAddress = dbh.drGetString("EmailAddress");
					item.Telephone = dbh.drGetString("Telephone");
					item.FullName = dbh.drGetString("FullName");
					item.Active = dbh.DrGetBoolean("Active");
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


		public CustomMembershipUser(UserModel user):base("CustomMembership", user.UserName, user.UserId, user.EmailAddress, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
			{
			UserId = user.UserId;
			FirstName = user.FirstName;
			LastName = user.LastName;
			Roles = user.Roles;
			}
	}

}