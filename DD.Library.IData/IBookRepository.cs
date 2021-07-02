using DD.Library.Model;
using DD.Library.Model.Mappings;
using DD.Library.Model.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DD.Library.IData
{
	public interface IBookRepository
	{
		Task<Book> GetById(int id);
		Task Create(Book newBook);
		Task Edit(BookUpdate editedBook);
		Task PutInWardrobe(BookInWardrobe bookInWardrobeRequest);
		Task<List<BookView>> FullTextSearch(FullSearch searchText);
	}
}
