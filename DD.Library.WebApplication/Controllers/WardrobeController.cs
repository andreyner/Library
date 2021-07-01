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
			this.WardrobeReository = new WardrobeReository();
		}
		private readonly IMapper AutoMapper;
		private readonly WardrobeReository WardrobeReository;
		public string Index()
		{
			return $"Hello from Library!";
		}
		[HttpPost]
		public async Task<IActionResult> CreateWardrobe([FromBody] WardrobeCreating wardrobe)
		{
			Validator<WardrobeCreating>.CheckValid(wardrobe);
			var wardrobeDestination = AutoMapper.Map<Wardrobe>(wardrobe);
			var creatingTask=WardrobeReository.Create(wardrobeDestination);
			await Task.WhenAll(creatingTask);
			return Ok();
		}
		[HttpGet]
		public async Task<IActionResult> GetWardrobe(int id)
		{			
			var gettingTask= WardrobeReository.GetById(id);
			await Task.WhenAll(gettingTask);
			return Ok(gettingTask.Result);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateWardrobe([FromBody] WardrobeUpdate wardrobe)
		{
			Validator<WardrobeUpdate>.CheckValid(wardrobe);
			var wardrobeDestination = AutoMapper.Map<Wardrobe>(wardrobe);
			var updateingTask = WardrobeReository.Edit(wardrobeDestination);
			await Task.WhenAll(updateingTask);
			return Ok();
		}
	}
}
