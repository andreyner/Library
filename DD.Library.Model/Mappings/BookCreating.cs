using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DD.Library.Model.Mappings
{
	public class BookCreating
	{
		[Required]
		[MinLength(2)]
		public string Name { get; set; }

		[Required]
		[Min(1)]
		public int AuthorId { get; set; }

		public int ? WardrobeId { get; set; }
	}
}
