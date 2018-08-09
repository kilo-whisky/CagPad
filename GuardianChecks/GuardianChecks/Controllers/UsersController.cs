using GuardianChecks.Helpers;
using GuardianChecks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static GuardianChecks.Helpers.Authentication;

namespace GuardianChecks.Controllers
{
	[Authorize]
    public class UsersController : Controller
    {
		// GET: Users

		public ActionResult ChangePassword(int UserId, string ReturnUrl = null)
		{
			PasswordReset p = new PasswordReset();
			p.UserId = UserId;
			return View(p);
		}

		[HttpPost]
		public ActionResult ChangePassword(PasswordReset p, string ReturnUrl = null)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("", "Passwords do not match");
				return View(p);
			}
			else
			{
				string salt = LoginInfo.getLogin(p.UserName, null).Salt;
				string hash = PasswordHash(p.CurrentPassword, salt);
				LoginInfo login = LoginInfo.getLogin(p.UserName, hash);
				if (login.Match)
				{
					string newsalt = Authentication.CreateSalt(10);
					string newhash = PasswordHash(p.Password, newsalt);
					UserModel u = new UserModel()
					{
						UserId = p.UserId,
						UserName = p.UserName,
						Password = newhash,
						Salt = newsalt
					};
					u.upsert();
					return RedirectToAction("LogOut", "Security");
				}
				else
				{
					return View(p);
				}
			}
		}
			
		
		public ActionResult Edit(int UserId, string ReturnUrl = null)
		{
			return View(UserModel.GetByUserId(UserId));
		}

		[HttpPost]
		public ActionResult Edit(UserModel u, string ReturnUrl = null)
		{
			try
			{
				u.upsert();
				if (Url.IsLocalUrl(ReturnUrl))
				{
					return Redirect(ReturnUrl);
				}
				else
				{
					return RedirectToAction("Index", "Home");
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}