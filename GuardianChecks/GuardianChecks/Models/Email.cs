using Postal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuardianChecks.Models
{
	public class IssueEmail : Email
	{
		public List<string> Administrators { get; set; }
		public string ReportedByEmail { get; set; }
		public string Location { get; set; }
		public string Description { get; set; }
		public int Severity { get; set; }
		public string SeverityName { get; set; }
		public string SeverityDescription { get; set; }
		public string ReportedBy { get; set; }
	}
}