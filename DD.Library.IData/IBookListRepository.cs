using DD.Library.Model;
using DD.Library.Model.Mappings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DD.Library.IData
{
	public interface IBookListRepository
	{
		Task<List<BookView>> GetAllBooks();
		Task Create(List<BookCreating> newBooks);
		Task Edit(List<BookUpdate> editedBooks);
	}
}
