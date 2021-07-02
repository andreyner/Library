using AutoMapper;
using DD.Library.IData;
using DD.Library.Model;
using DD.Library.Model.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace DD.Library.Data
{
	public class BookListRepository : Repository, IBookListRepository
	{
		public BookListRepository(IMapper mapper) : base(mapper)
		{
		}

		public async Task Create(List<BookCreating> newBooks)
		{
			using (LibraryDbContext dbContext = new LibraryDbContext())
			{
				foreach (var book in newBooks)
				{
					var author = dbContext.Authors.AsNoTracking().FirstOrDefault(x => x.Id == book.AuthorId);
					if (author == null)
					{
						throw new Exception($"Не найден автор с id {book.AuthorId}");
					}
					if(book.WardrobeId.HasValue)
					{
						var wardrobe = dbContext.Wardrobes.AsNoTracking().FirstOrDefault(x => x.Id == book.WardrobeId.Value);
						if (wardrobe == null)
						{
							throw new Exception($"Не найден стилаж с id {book.WardrobeId.Value}");
						}
					}
				}
				List<Book> books = new List<Book>();
				AutoMapper.Map(newBooks, books);
				dbContext.Books.AddRange(books);
				await dbContext.SaveChangesAsync();
			}
		}

		public async Task Edit(List<BookUpdate> editedBooks)
		{
			using (LibraryDbContext dbContext = new LibraryDbContext())
			{
				foreach (var editedBook in editedBooks)
				{
					var destinationBook = dbContext.Books.AsNoTracking().FirstOrDefault(x => x.Id == editedBook.Id);
					if (destinationBook == null) { throw new Exception($"Книга с id {editedBook.Id} не найдена!"); }
					var author = dbContext.Authors.AsNoTracking().FirstOrDefault(x => x.Id == editedBook.AuthorId);
					if (author == null)
					{ throw new Exception($"Автор с id {editedBook.AuthorId} не найден!"); }
					var booksDestination = AutoMapper.Map(editedBook, destinationBook);
					dbContext.Update(booksDestination);
				}			
				await dbContext.SaveChangesAsync();
			}
		}
		public async Task<List<BookView>> GetAllBooks()
		{
			using (LibraryDbContext dbContext = new LibraryDbContext())
			{
				var taskListBook=dbContext.Books.AsNoTracking().ToListAsync();
				await taskListBook;
				return AutoMapper.Map<List<BookView>>(taskListBook.Result);
			}
		}
	}
}
