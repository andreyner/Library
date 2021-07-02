using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DD.Library.Model.Mappings
{
	public class BookUpdate
	{
		[Required]
		[Min(1)]
		public int Id { get; set; }
		[Required]
		[MinLength(2)]
		public string Name { get; set; }

		[Required]
		public int AuthorId { get; set; }
	}
}
