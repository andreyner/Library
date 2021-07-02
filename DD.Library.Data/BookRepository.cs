using AutoMapper;
using DD.Library.IData;
using DD.Library.Model;
using DD.Library.Model.Mappings;
using DD.Library.Model.Requests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD.Library.Data
{
	public class BookRepository : Repository, IBookRepository
	{
		public BookRepository(IMapper mapper) : base(mapper)
		{
		}
		public async Task Create(Book newBook)
		{
			using (LibraryDbContext dbContext = new LibraryDbContext())
			{
				if (newBook.WardrobeId.HasValue)
				{
					var wardrobe = dbContext.Wardrobes.FirstOrDefault(x => x.Id == newBook.WardrobeId);
					if (wardrobe == null)
					{ throw new Exception($"Стилаж с id {newBook.WardrobeId} не найден!"); }
				}
				var author = dbContext.Authors.FirstOrDefault(x => x.Id == newBook.AuthorId);
				if (author == null)
				{ throw new Exception($"Автор с id {newBook.AuthorId} не найден!"); }

				dbContext.Books.Add(newBook);
				await dbContext.SaveChangesAsync();
			}
		}
		public async Task Edit(BookUpdate editedBook)
		{
			using (LibraryDbContext dbContext = new LibraryDbContext())
			{
				var destinationBook = dbContext.Books.FirstOrDefault(x => x.Id == editedBook.Id);
				if (destinationBook == null) { throw new Exception($"Книга с id {editedBook.Id} не найдена!"); }
				var author = dbContext.Authors.AsNoTracking().FirstOrDefault(x => x.Id == editedBook.AuthorId);
				if (author == null)
				{ throw new Exception($"Автор с id {editedBook.AuthorId} не найден!"); }
				var wardrobeDestination = AutoMapper.Map(editedBook, destinationBook);
				dbContext.Update(destinationBook);
				await dbContext.SaveChangesAsync();
			}
		}
		public async Task<List<BookView>> FullTextSearch(FullSearch searchText)
		{
			return await Task<List<Book>>.Run(() =>
			  {
				  List<Book> searchResult = new List<Book>();
				  List<BookView> searchResultView = new List<BookView>();
				  var taskSearchBykBook = Task<List<Book>>.Run(() =>
				  {
					  using (LibraryDbContext dbContext = new LibraryDbContext())
					  {
						  // EF.Functions.FreeText is better
						  return  dbContext.Books.AsNoTracking().Where(x => EF.Functions.Like(x.Name, $"%{searchText.SearchText}%")).Include(x=>x.Author).ToList();
					  }
				  });
				  var taskSearchByAuthor = Task<List<Book>>.Run(() =>
				  {
					  using (LibraryDbContext dbContext = new LibraryDbContext())
					  {
						  List<Book> books = new List<Book>();
						  var authors = dbContext.Authors.AsNoTracking().Where(x => EF.Functions.Like(x.LastName, $"%{searchText.SearchText}%")).ToList();
						  foreach (var author in authors)
						  {
							  books.AddRange(dbContext.Books.Where(x => x.AuthorId == author.Id).Include(x => x.Author).ToList());
						  }
						  return books;
					  }
				  });
				  Task.WaitAll(taskSearchBykBook, taskSearchByAuthor);
				  searchResult.AddRange(taskSearchBykBook.Result);
				  searchResult.AddRange(taskSearchByAuthor.Result);
				  AutoMapper.Map(searchResult, searchResultView);
				  return searchResultView;
			  });
		}
		public async Task<Book> GetById(int id)
		{
			return await Task.Run(() =>
			{
				using (LibraryDbContext dbContext = new LibraryDbContext())
				{
					var book = dbContext.Books.AsNoTracking().Where(x => x.Id == id)
					.Include(x => x.Author)
					.Include(x => x.Wardrobe).FirstOrDefault();
					if (book == null)
					{
						throw new Exception($"Книга с id {id} не найдена!");
					}
					return book;
				}
			});
		}
		public async Task PutInWardrobe(BookInWardrobe bookInWardrobeRequest)
		{
			using (LibraryDbContext dbContext = new LibraryDbContext())
			{
				var destinationBook = dbContext.Books.FirstOrDefault(x => x.Id == bookInWardrobeRequest.BookId);
				if (destinationBook == null) { throw new Exception($"Книга с id {bookInWardrobeRequest.BookId} не найдена!"); }
				var destinationWardrobe = dbContext.Wardrobes.FirstOrDefault(x => x.Id == bookInWardrobeRequest.WardrobeId);
				if (destinationWardrobe == null) { throw new Exception($"Стилаж с id {bookInWardrobeRequest.WardrobeId} не найден!"); }
				destinationBook.WardrobeId = bookInWardrobeRequest.WardrobeId;
				await dbContext.SaveChangesAsync();
			}
		}
	}
}
