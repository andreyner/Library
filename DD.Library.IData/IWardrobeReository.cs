using DD.Library.Model;
using System;
using System.Threading.Tasks;

namespace DD.Library.IData
{
	public interface IWardrobeReository
	{
		Task<Wardrobe> GetById(int id);
		Task Create(Wardrobe newWardrobe);
		Task Edit(Wardrobe editWardrobe);
	}
}
