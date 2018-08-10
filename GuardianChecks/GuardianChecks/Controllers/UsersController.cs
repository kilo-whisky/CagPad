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
					UserModel u = new UserModel()
					{
						UserId = p.UserId,
						UserName = p.UserName,
						Password = p.Password,
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

		public ActionResult AdminResetPassword (int UserId)
		{
			PasswordReset p = new PasswordReset();
			p.UserId = UserId;
			return View(p);
		}

		[HttpPost]
		public ActionResult AdminResetPassword(PasswordReset p)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("", "Passwords do not match");
				return View(p);
			}
			else
			{
				string newsalt = Authentication.CreateSalt(10);
				UserModel u = new UserModel()
				{
					UserId = p.UserId,
					UserName = p.UserName,
					Password = p.Password,
					Salt = newsalt
				};
				u.upsert();
				return RedirectToAction("Users", "Users");
			}
		}

		public ActionResult Create()
		{
			UserModel u = new UserModel();
			u.Active = true;
			return View(u);
		}

		[HttpPost]
		public ActionResult Create(UserModel u)
		{
			if (!ModelState.IsValid)
			{
				return View(u);
			}
			u.Salt = Authentication.CreateSalt(10);
			u.upsert();
			return RedirectToAction("Users", "Users");
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

		public ActionResult Users()
		{
			return View(UserModel.GetUsers());
		}
	}
}