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
			this.WardrobeRepository = new WardrobeRepository(mapper);
		}
		private readonly IMapper AutoMapper;
		private readonly WardrobeRepository WardrobeRepository;
		public string Index()
		{
			return $"Hello from Library!";
		}
		[HttpPost]
		public async Task<IActionResult> CreateWardrobe([FromBody] WardrobeCreating wardrobe)
		{
			Validator<WardrobeCreating>.CheckValid(wardrobe);
			var wardrobeDestination = AutoMapper.Map<Wardrobe>(wardrobe);
			var creatingTask=WardrobeRepository.Create(wardrobeDestination);
			await Task.WhenAll(creatingTask);
			return Ok();
		}
		[HttpGet]
		public async Task<IActionResult> GetWardrobe(int id)
		{			
			var gettingTask= WardrobeRepository.GetById(id);
			await Task.WhenAll(gettingTask);
			return Ok(gettingTask.Result);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateWardrobe([FromBody] WardrobeUpdate wardrobe)
		{
			Validator<WardrobeUpdate>.CheckValid(wardrobe);
			var updateingTask = WardrobeRepository.Edit(wardrobe);
			await Task.WhenAll(updateingTask);
			return Ok();
		}
	}
}
