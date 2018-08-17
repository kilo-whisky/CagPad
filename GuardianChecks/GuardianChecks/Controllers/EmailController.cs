using GuardianChecks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace GuardianChecks.Controllers
{
	public class EmailController : Controller
	{
		public ActionResult DistributeEmail()
		{
			SendIssueMail(1);
			SendIssueMail(2);
			SendIssueMail(3);
			return RedirectToAction("Index", "Home");
		}

		// GET: Email
		public void SendIssueMail(int IssueId)
		{
			Issue i = Issue.GetIssues(IssueId, null, null, null, null, null)[0];
			MailMessage msg = new MailMessage();
			MailAddress fromAddress = new MailAddress("cagpad@gmail.com", "Cardiac Action Group");
			var Administrators = UserModel.GetByRole("SYSADMIN").Select(x => x.EmailAddress).ToList();
			foreach(var address in Administrators)
			{
				msg.To.Add(address);
			}
			const string fromPassword = "P@ssw0rd88";
			msg.Subject = "Issue reported for " + i.PadSite + " PAD site";
			msg.CC.Add(new MailAddress(i.ReportedByEmail));
			msg.Body = "Hello";

			var smtp = new SmtpClient
			{
				Host = "smtp.gmail.com",
				Port = 465,
				EnableSsl = true,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
			};
			smtp.EnableSsl = true;
			smtp.Send(msg);
		}
	}
}