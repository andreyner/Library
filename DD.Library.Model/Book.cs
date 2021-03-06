using System;
using System.ComponentModel.DataAnnotations;

namespace DD.Library.Model
{
	public class Book
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[MinLength(2)]
		public string Name { get; set; }

		[Required]
		public int AuthorId { get; set; }
		public Author Author { get; set; }

		public int ? WardrobeId { get; set; }
		public Wardrobe Wardrobe { get; set; }

	}
}
