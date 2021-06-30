using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DD.Library.Model.Mappings
{
	public class WardrobeView
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }
	}
}
