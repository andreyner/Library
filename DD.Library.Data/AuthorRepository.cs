using DD.Library.IData;
using DD.Library.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DD.Library.Data
{
	public class AuthorRepository : IAuthorRepository
	{
		public AuthorRepository()
		{ 
		}
		public async Task Create(Author author)
		{
			using (LibraryDbContext dbContext = new LibraryDbContext())
			{
				dbContext.Authors.Add(author);
				await dbContext.SaveChangesAsync();
			}
		}
	}
}
