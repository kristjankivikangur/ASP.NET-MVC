using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JalgrattaEksam.Models
{
	public class LubaViewModel
	{
		public int Id { get; set; }
		public string Eesnimi { get; set; }
		public string Perenimi { get; set; }
		public int Teooria { get; set; } 
		public string Slaalom { get; set; }
		public string Ringtee { get; set; }
		public string Tee { get; set; }
		public int Luba { get; set; }
	}
}