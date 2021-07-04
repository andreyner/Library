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
			CreateMap<Wardrobe, WardrobeView>();
			CreateMap<WardrobeCreating, Wardrobe>();
			CreateMap<WardrobeUpdate, Wardrobe>();
			CreateMap<BookView, Book>();
			CreateMap<Book,BookView>();
			CreateMap<BookCreating, Book>();
			CreateMap<BookUpdate, Book>();
			CreateMap<AuthorCreating, Author>();
		}
	}
}
