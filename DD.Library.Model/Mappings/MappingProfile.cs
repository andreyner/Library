using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Library.Model.Mappings
{
	public class MappingProfile: Profile
	{
		public MappingProfile()
		{
			CreateMap<WardrobeView, Wardrobe>();
			CreateMap<WardrobeCreating, Wardrobe>();
			CreateMap<WardrobeUpdate, Wardrobe>();
		}
	}
}
