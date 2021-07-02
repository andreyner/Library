﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DD.Library.Model
{
	public class Wardrobe
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name{ get; set; }
	}
}
