using AutoMapper;
using DD.Library.Data;
using DD.Library.Model;
using DD.Library.Model.Mappings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DD.Library.WebApplication.Controllers
{

	public class WardrobeController : ControllerBase
	{		
		public WardrobeController (IMapper mapper)
		{
			this.AutoMapper = mapper;
		}
		private readonly IMapper AutoMapper;
		public string Index()
		{
			return "Hello from Library!";
		}
		[HttpPost]
		public IActionResult CreateWardrobe([FromBody] WardrobeCreating wardrobe)
		{
			Validator<WardrobeCreating>.CheckValid(wardrobe);
			var mappedresult = AutoMapper.Map<Wardrobe>(wardrobe);
			return Ok();
		}
	}
}
