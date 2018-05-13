using GuardianChecks.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuardianChecks.Models
{
	public class Navbar
	{
		public int NavId { get; set; }
		public string Name { get; set; }
		public string Controller { get; set; }
		public string Action { get; set; }
		public bool Active { get; set; }
		public string Icon { get; set; }
		public int ParentId { get; set; }
		public bool isParent { get; set; }

		public static List<Navbar> GetNav(int? NavId, int? ParentId)
		{
			List<Navbar> list = new List<Navbar>();
			using (dbHelp dbh = new dbHelp("Core.Navigation_List", true, "CAG"))
			{
				dbh.addParam("NavId", NavId);
				//dbh.addParam("ParentNavId", ParentId);
				//dbh.addParam("UserId", (int)HttpContext.Current.Session["UserId"]);
				while (dbh.dr.Read())
				{
					Navbar item = new Navbar();
					item.NavId = dbh.drGetInt32("NavId");
					item.Name = dbh.drGetString("Name");
					item.Controller = dbh.drGetString("Controller");
					item.Action = dbh.drGetString("Action");
					item.Icon = dbh.drGetString("Icon");
					item.Active = dbh.DrGetBoolean("Active");
					item.isParent = dbh.DrGetBoolean("isParent");
					item.ParentId = dbh.drGetInt32("ParentId");
					list.Add(item);
				}
			}
			return list;
		}
	}
}