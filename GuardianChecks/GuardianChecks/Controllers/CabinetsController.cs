using GuardianChecks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuardianChecks.Controllers
{
	public class CabinetsController : Controller
	{
		// GET: Cabinets
		public ActionResult Index()
		{
			return View(Cabinet.GetCabinets(null));
		}

		public ActionResult Details(int CabinetId)
		{
			return View(Cabinet.GetCabinets(CabinetId).First());
		}
	}
}