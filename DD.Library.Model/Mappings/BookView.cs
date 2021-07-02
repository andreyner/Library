using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DD.Library.Model.Mappings
{
	public class BookView
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public Author Author { get; set; }
		public Wardrobe Wardrobe { get; set; }
	}
}
