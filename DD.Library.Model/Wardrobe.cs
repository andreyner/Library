using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DD.Library.Model
{
	public class Wardrobe
	{
		public Wardrobe()
		{
			Books = new List<Book>();
		}
		public int Id { get; set; }
		[Required]
		public string Name{ get; set; }
		public ICollection<Book> Books { get; set; }
	}
}
