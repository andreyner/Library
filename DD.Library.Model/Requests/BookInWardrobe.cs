using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DD.Library.Model.Requests
{
	public class BookInWardrobe
	{
		[Required]
		[Min(1)]
		public int BookId { get; set; }
		[Required]
		[Min(1)]
		public int WardrobeId { get; set; }
	}
}
