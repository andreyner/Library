using DD.Library.Model;
using System;

namespace DD.Library.IData
{
	public interface IWardrobe
	{
		Wardrobe GetById(int id);
		void Update(int id);
	}
}
