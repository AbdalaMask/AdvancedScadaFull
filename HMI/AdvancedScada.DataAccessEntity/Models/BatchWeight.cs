 
using System;

namespace AdvancedScada.DataAccessEntity.Models
{
	public  class BatchWeight
	{
		public int BatchID {get; set;}
		public string BatchName {get; set;}
		public string TankName {get; set;}
		public float? FinalWeight {get; set;}
		public int? Works {get; set;}
		public DateTime? DateT {get; set;}
		public string TimeT {get; set;}

	}

}