using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DD.Library.Model.Requests
{
	public class FullSearch
	{
		[Required]
		public string SearchText { get; set; }
	}
}
