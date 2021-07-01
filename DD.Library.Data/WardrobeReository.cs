using DD.Library.IData;
using DD.Library.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DD.Library.Data
{
	public class WardrobeReository : IWardrobeReository
	{
		public async Task Create(Wardrobe newWardrobe)
		{
			using(LibraryDbContext dbContext=new LibraryDbContext()) 
			{
				dbContext.Wardrobes.Add(newWardrobe);
				await dbContext.SaveChangesAsync();
			}
			
		}

		public async Task Edit(Wardrobe editWardrobe)
		{
			using (LibraryDbContext dbContext = new LibraryDbContext())
			{
				dbContext.Set<Wardrobe>().Attach(editWardrobe);
				dbContext.Entry(editWardrobe).State = EntityState.Modified;
				await dbContext.SaveChangesAsync();
			}
		}

		public async Task<Wardrobe> GetById(int id)
		{
			return await Task.Run(() =>
			{
				using (LibraryDbContext dbContext = new LibraryDbContext())
				{
					var wardrobe = dbContext.Wardrobes.Where(x => x.Id == id).FirstOrDefault();
					if (wardrobe == null)
					{
						throw new Exception($"Стилаж с id {id} не найден!");
					}
					return wardrobe;
				}
			});
		}
	}
}
