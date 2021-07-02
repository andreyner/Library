using DD.Library.IData;
using DD.Library.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using DD.Library.Model.Mappings;

namespace DD.Library.Data
{
	public class WardrobeRepository: Repository, IWardrobeRepository
	{
		public WardrobeRepository(IMapper mapper) : base(mapper)
		{
		}

		public async Task Create(Wardrobe newWardrobe)
		{
			using(LibraryDbContext dbContext=new LibraryDbContext()) 
			{
				dbContext.Wardrobes.Add(newWardrobe);
				await dbContext.SaveChangesAsync();
			}
			
		}

		public async Task Edit(WardrobeUpdate editedWardrobe)
		{
			using (LibraryDbContext dbContext = new LibraryDbContext())
			{
				var destinationWardrobe = dbContext.Wardrobes.FirstOrDefault(x => x.Id == editedWardrobe.Id);
				if (destinationWardrobe == null) { throw new Exception($"Стилаж с id {editedWardrobe.Id} не найден!"); }
				var wardrobeDestination = AutoMapper.Map(editedWardrobe, destinationWardrobe);
				dbContext.Update(destinationWardrobe);
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
