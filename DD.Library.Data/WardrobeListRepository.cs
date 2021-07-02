using AutoMapper;
using DD.Library.IData;
using DD.Library.Model;
using DD.Library.Model.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD.Library.Data
{
	public class WardrobeListRepository : Repository, IWardrobeListRepository
	{
		public WardrobeListRepository(IMapper mapper) : base(mapper)
		{
		}

		public async Task Create(List<WardrobeCreating> newWardrobes)
		{
			using (LibraryDbContext dbContext = new LibraryDbContext())
			{
				List<Book> books = new List<Book>();			
				dbContext.Wardrobes.AddRange(AutoMapper.Map<List<Wardrobe>>(newWardrobes));
				await dbContext.SaveChangesAsync();
			}
		}

		public async Task Edit(List<WardrobeUpdate> editedWardrobes)
		{
			using (LibraryDbContext dbContext = new LibraryDbContext())
			{
				foreach (var editedWardrobe in editedWardrobes)
				{
					var wardrobe=dbContext.Wardrobes.FirstOrDefault(x => x.Id == editedWardrobe.Id);
					if(wardrobe==null)
					{
						throw new Exception($"Стилаж с id {editedWardrobe.Id} не найден!");
					}
					var wardrobeDestination = AutoMapper.Map(editedWardrobe, wardrobe);
					dbContext.Update(wardrobeDestination);
				}
				await dbContext.SaveChangesAsync();
			}
		}

		public async Task<List<WardrobeView>> GetAllWardrobes()
		{
			using (LibraryDbContext dbContext = new LibraryDbContext())
			{
				var taskListWardrobe = dbContext.Wardrobes.AsNoTracking().ToListAsync();
				await taskListWardrobe;
				return AutoMapper.Map<List<WardrobeView>>(taskListWardrobe.Result);
			}
		}
	}
}
