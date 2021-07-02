using DD.Library.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DD.Library.IData
{
	public interface IAuthorRepository
	{
		Task Create(Author author);
	}
}
