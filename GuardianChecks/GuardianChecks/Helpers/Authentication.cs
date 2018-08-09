using GuardianChecks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

namespace GuardianChecks.Helpers
{
	public class Authentication : MembershipProvider
	{

		public class LoginInfo
		{
			public int? UserId { get; set; }
			public string UserName { get; set; }
			public string FullName { get; set; }
			public string Salt { get; set; }
			public string Password { get; set; }
			public bool Match { get; set; }


			public static LoginInfo getLogin(string Username, string Password)
			{
				LoginInfo login = new LoginInfo();
				using (dbHelp dbh = new dbHelp("Core.User_login", true, "CAG"))
				{
					dbh.addParam("UserName", Username);
					dbh.addParam("Password", Password);
					while (dbh.dr.Read())
					{
						LoginInfo item = new LoginInfo();
						item.UserId = dbh.drGetInt32Null("UserId");
						item.UserName = dbh.drGetString("UserName");
						item.FullName = dbh.drGetString("FullName");
						item.Salt = dbh.drGetString("Salt");
						item.Match = dbh.DrGetBoolean("Match");
						login = item;
					}
					return login;
				}
			}
		}

		public static string ByteArrayToHexString(byte[] ba)
		{
			StringBuilder hex = new StringBuilder(ba.Length * 2);
			foreach (byte b in ba)
				hex.AppendFormat("{0:x2}", b);
			return hex.ToString();
		}

		public static string CreateSalt(int size)
		{
			var rng = new RNGCryptoServiceProvider();
			var buff = new byte[size];
			rng.GetBytes(buff);
			return Convert.ToBase64String(buff);
		}

		public static string PasswordHash(string password, string salt)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(password + salt);
			SHA256Managed sha256hashstring = new SHA256Managed();
			byte[] hash = sha256hashstring.ComputeHash(bytes);
			return ByteArrayToHexString(hash);
		}

		public override bool ValidateUser(string username, string password)
		{
			if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
			{
				return false;
			}

			LoginInfo getsalt = LoginInfo.getLogin(username, null);
			string hashedpass = PasswordHash(password, getsalt.Salt);
			LoginInfo l = LoginInfo.getLogin(username, hashedpass);
			HttpContext.Current.Session["UserId"] = l.UserId;
			HttpContext.Current.Session["UserName"] = l.UserName;
			HttpContext.Current.Session["UserFullName"] = l.FullName;
			return l.Match;
		}

		public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
		{
			throw new NotImplementedException();
		}

		public override MembershipUser GetUser(string username, bool userIsOnline)
		{
			UserModel user = UserModel.GetByUsername(username);

			if (user == null)
			{
				return null;
			}

			var selectedUser = new CustomMembershipUser(user);

			return selectedUser;

		}

		public override string GetUserNameByEmail(string email)
		{
			UserModel user = UserModel.GetByEmailAddress(email);
			return !string.IsNullOrEmpty(user.UserName) ? user.UserName : string.Empty;
		}

		#region Overrides of Membership Provider  

		public override string ApplicationName
		{
			get
			{
				throw new NotImplementedException();
			}

			set
			{
				throw new NotImplementedException();
			}
		}

		public override bool EnablePasswordReset
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override bool EnablePasswordRetrieval
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override int MaxInvalidPasswordAttempts
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override int MinRequiredNonAlphanumericCharacters
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override int MinRequiredPasswordLength
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override int PasswordAttemptWindow
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override MembershipPasswordFormat PasswordFormat
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override string PasswordStrengthRegularExpression
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override bool RequiresQuestionAndAnswer
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override bool RequiresUniqueEmail
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override bool ChangePassword(string username, string oldPassword, string newPassword)
		{
			throw new NotImplementedException();
		}

		public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
		{
			throw new NotImplementedException();
		}

		public override bool DeleteUser(string username, bool deleteAllRelatedData)
		{
			throw new NotImplementedException();
		}

		public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override int GetNumberOfUsersOnline()
		{
			throw new NotImplementedException();
		}

		public override string GetPassword(string username, string answer)
		{
			throw new NotImplementedException();
		}

		public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
		{
			throw new NotImplementedException();
		}

		public override string ResetPassword(string username, string answer)
		{
			throw new NotImplementedException();
		}

		public override bool UnlockUser(string userName)
		{
			throw new NotImplementedException();
		}

		public override void UpdateUser(MembershipUser user)
		{
			throw new NotImplementedException();
		}

		#endregion

	}
}