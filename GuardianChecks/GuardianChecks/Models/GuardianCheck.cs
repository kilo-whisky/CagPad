using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuardianChecks.Models
{
	public class GuardianCheck
	{
		public int PadId { get; set; }
		public DateTime Date { get; set; }
		public int UserId { get; set; }
		public bool CabinetOpenLock { get; set; }
		public bool CabinetBatteriesOk { get; set; }
		public bool CabinetLightWork { get; set; }
		public bool NothingTouchingHeater { get; set; }
		public bool AEDOk { get; set; }
		public bool AEDSilent { get; set; }
		public bool ResusKit { get; set; }


	}
}