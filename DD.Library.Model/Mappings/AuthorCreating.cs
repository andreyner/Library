﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DD.Library.Model.Mappings
{
	public class AuthorCreating
	{
		[Required]
		[MinLength(1)]
		public string FirstName { get; set; }
		[Required]
		[MinLength(1)]
		public string LastName { get; set; }
		public string Patronymic { get; set; }
	}
}
