using DD.Library.Model.Mappings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DD.Library.IData
{
	public interface IWardrobeListRepository
	{
		Task<List<WardrobeView>> GetAllWardrobes();
		Task Create(List<WardrobeCreating> newWardrobes);
		Task Edit(List<WardrobeUpdate> editedWardrobes);
	}
}
