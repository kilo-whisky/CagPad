using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using GuardianChecks.Helpers;
using GuardianChecks.Models;
using Newtonsoft.Json;
using static GuardianChecks.Helpers.Authentication;

namespace GuardianChecks.Controllers
{
	[AllowAnonymous]
	public class SecurityController : Controller
	{

		public ActionResult Login(string ReturnUrl = null)
		{
			UserModel u = new UserModel();
			return View(u);
		}

		[HttpPost]
		public ActionResult Login(UserModel u, string ReturnUrl = null)
		{
			string salt = LoginInfo.getLogin(u.UserName, null).Salt;
			string hash = PasswordHash(u.Password, salt);
			LoginInfo login = LoginInfo.getLogin(u.UserName, hash);
			if (login.Match)
			{
				Session["UserId"] = login.UserId;
				Session["UserName"] = login.UserName;
				Session["UserFullName"] = login.FullName;
				FormsAuthentication.SetAuthCookie(login.UserName, false);
				if (Url.IsLocalUrl(ReturnUrl))
				{
					return Redirect(ReturnUrl);
				}
				else
				{
					return RedirectToAction("Index", "Home");
				}
			}
			else
			{
				ModelState.AddModelError("", "Something Wrong : Username or Password invalid");
				return View(u);
			}
		}

		public ActionResult AccessDenied()
		{
			Response.StatusCode = 403;
			return View();
		}

		//[AllowAnonymous]
		//public ActionResult UpsertMe()
		//{
		//	Authentication a = new Authentication();
		//	UserModel u = new UserModel
		//	{
		//		UserId = 1,
		//		UserName = "KWOOD",
		//		FirstName = "Kyle",
		//		LastName = "Wood",
		//		EmailAddress = "steel.woodrow@gmail.com",
		//		Telephone = "07839111653",
		//		Active = true,
		//		Password = "BimBle88",
		//		Salt = Authentication.CreateSalt(10)
		//	};
		//	u.upsert();
		//	return Json("Done", JsonRequestBehavior.AllowGet);
		//}

		//[HttpGet]
		//public ActionResult Login(string ReturnUrl = "")
		//{
		//	if (User.Identity.IsAuthenticated)
		//	{
		//		return LogOut();
		//	}
		//	ViewBag.ReturnUrl = ReturnUrl;
		//	return View();
		//}

		//[HttpPost]
		//public ActionResult Login(LoginView loginView, string ReturnUrl = "")
		//{
		//	if (ModelState.IsValid)
		//	{
		//		if (Membership.ValidateUser(loginView.UserName, loginView.Password))
		//		{
		//			var user = (CustomMembershipUser)Membership.GetUser(loginView.UserName, false);
		//			if (user != null)
		//			{
		//				CustomSerializeModel userModel = new Models.CustomSerializeModel()
		//				{
		//					UserId = user.UserId,
		//					FirstName = user.FirstName,
		//					LastName = user.LastName,
		//					RoleName = user.Roles.Select(r => r.RoleName).ToList()
		//				};

		//				string userData = JsonConvert.SerializeObject(userModel);
		//				FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
		//						(
		//							1, loginView.UserName, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData
		//						);

		//				string enTicket = FormsAuthentication.Encrypt(authTicket);
		//				HttpCookie faCookie = new HttpCookie("Cookie1", enTicket);
		//				Response.Cookies.Add(faCookie);
		//			}

		//			if (Url.IsLocalUrl(ReturnUrl))
		//			{
		//				return Redirect(ReturnUrl);
		//			}
		//			else
		//			{
		//				return RedirectToAction("Index");
		//			}
		//		}
		//	}
		//	ModelState.AddModelError("", "Something Wrong : Username or Password invalid ^_^ ");
		//	return View(loginView);
		//}

		////[HttpGet]
		////public ActionResult Registration()
		////{
		////	return View();
		////}

		////[HttpPost]
		////public ActionResult Registration(RegistrationView registrationView)
		////{
		////	bool statusRegistration = false;
		////	string messageRegistration = string.Empty;

		////	if (ModelState.IsValid)
		////	{
		////		// Email Verification  
		////		string userName = Membership.GetUserNameByEmail(registrationView.Email);
		////		if (!string.IsNullOrEmpty(userName))
		////		{
		////			ModelState.AddModelError("Warning Email", "Sorry: Email already Exists");
		////			return View(registrationView);
		////		}

		////		//Save User Data   
		////		//using (AuthenticationDB dbContext = new AuthenticationDB())
		////		//{
		////		//	var user = new User()
		////		//	{
		////		//		Username = registrationView.Username,
		////		//		FirstName = registrationView.FirstName,
		////		//		LastName = registrationView.LastName,
		////		//		Email = registrationView.Email,
		////		//		Password = registrationView.Password,
		////		//		ActivationCode = Guid.NewGuid(),
		////		//	};

		////		//	dbContext.Users.Add(user);
		////		//	dbContext.SaveChanges();
		////		//}

		////		//Verification Email  
		////		VerificationEmail(registrationView.Email, registrationView.ActivationCode.ToString());
		////		messageRegistration = "Your account has been created successfully. ^_^";
		////		statusRegistration = true;
		////	}
		////	else
		////	{
		////		messageRegistration = "Something Wrong!";
		////	}
		////	ViewBag.Message = messageRegistration;
		////	ViewBag.Status = statusRegistration;

		////	return View(registrationView);
		////}

		////[HttpGet]
		////public ActionResult ActivationAccount(string id)
		////{
		////	bool statusAccount = false;
		////	//using (AuthenticationDB dbContext = new DataAccess.AuthenticationDB())
		////	//{
		////	//	var userAccount = dbContext.Users.Where(u => u.ActivationCode.ToString().Equals(id)).FirstOrDefault();

		////	//	if (userAccount != null)
		////	//	{
		////	//		userAccount.IsActive = true;
		////	//		dbContext.SaveChanges();
		////	//		statusAccount = true;
		////	//	}
		////	//	else
		////	//	{
		////	//		ViewBag.Message = "Something Wrong !!";
		////	//	}

		////	//}
		////	ViewBag.Status = statusAccount;
		////	return View();
		////}

		public ActionResult LogOut()
		{
			FormsAuthentication.SignOut();
			Session.Clear();
			return RedirectToAction("Login", "Security");
		}

		////[NonAction]
		////public void VerificationEmail(string email, string activationCode)
		////{
		////	var url = string.Format("/Account/ActivationAccount/{0}", activationCode);
		////	var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, url);

		////	var fromEmail = new MailAddress("mehdi.rami2012@gmail.com", "Activation Account - AKKA");
		////	var toEmail = new MailAddress(email);

		////	var fromEmailPassword = "******************";
		////	string subject = "Activation Account !";

		////	string body = "<br/> Please click on the following link in order to activate your account" + "<br/><a href='" + link + "'> Activation Account ! </a>";

		////	var smtp = new SmtpClient
		////	{
		////		Host = "smtp.gmail.com",
		////		Port = 587,
		////		EnableSsl = true,
		////		DeliveryMethod = SmtpDeliveryMethod.Network,
		////		UseDefaultCredentials = false,
		////		Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
		////	};

		////	using (var message = new MailMessage(fromEmail, toEmail)
		////	{
		////		Subject = subject,
		////		Body = body,
		////		IsBodyHtml = true

		////	})

		////		smtp.Send(message);

		////}
	}
}