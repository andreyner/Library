using AutoMapper;
using DD.Library.Data;
using DD.Library.Model;
using DD.Library.Model.Mappings;
using DD.Library.WebApplication.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DD.Library.WebApplication.Controllers
{
	[Route("[controller]")]
	[ApiController]
	[CustomExceptionFilter]
	public class WardrobeController : ControllerBase
	{		
		public WardrobeController (IMapper mapper)
		{
			this.AutoMapper = mapper;
			this.WardrobeRepository = new WardrobeRepository(mapper);
		}
		private readonly IMapper AutoMapper;
		private readonly WardrobeRepository WardrobeRepository;

		[HttpPost(nameof(CreateWardrobe))]
		public async Task<IActionResult> CreateWardrobe([FromBody] WardrobeCreating wardrobe)
		{
			Validator<WardrobeCreating>.CheckValid(wardrobe);
			var wardrobeDestination = AutoMapper.Map<Wardrobe>(wardrobe);
			var creatingTask=WardrobeRepository.Create(wardrobeDestination);
			await Task.WhenAll(creatingTask);
			return Ok();
		}
		[HttpGet(nameof(GetWardrobe))]
		public async Task<IActionResult> GetWardrobe(int id)
		{			
			var gettingTask= WardrobeRepository.GetById(id);
			await Task.WhenAll(gettingTask);
			return Ok(gettingTask.Result);
		}

		[HttpPut(nameof(UpdateWardrobe))]
		public async Task<IActionResult> UpdateWardrobe([FromBody] WardrobeUpdate wardrobe)
		{
			Validator<WardrobeUpdate>.CheckValid(wardrobe);
			var updateingTask = WardrobeRepository.Edit(wardrobe);
			await Task.WhenAll(updateingTask);
			return Ok();
		}
	}
}
