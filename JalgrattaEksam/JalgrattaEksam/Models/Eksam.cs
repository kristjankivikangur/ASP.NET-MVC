using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace JalgrattaEksam.Models
{
	public class Eksam
	{
		public int Id { get; set; }
		public string Eesnimi { get; set; }
		public string Perenimi { get; set; }
		[Range(-1, 10)]
		public int Teooria { get; set; } = -1;
		public int Slaalom { get; set; } = -1;
		public int Ringtee { get; set; } = -1;
		public int Tee { get; set; } = -1;
		public int Luba { get; set; } = -1;
	}
}