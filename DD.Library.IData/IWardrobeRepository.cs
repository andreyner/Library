using DD.Library.Model;
using DD.Library.Model.Mappings;
using System;
using System.Threading.Tasks;

namespace DD.Library.IData
{
	public interface IWardrobeRepository
	{
		Task<Wardrobe> GetById(int id);
		Task Create(Wardrobe newWardrobe);
		Task Edit(WardrobeUpdate editedWardrobe);
	}
}
