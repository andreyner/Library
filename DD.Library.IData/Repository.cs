using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Library.IData
{
	public abstract class Repository
	{
		public Repository(IMapper mapper)
		{
			this.AutoMapper = mapper;
		}
		protected readonly IMapper AutoMapper;
	}
}
