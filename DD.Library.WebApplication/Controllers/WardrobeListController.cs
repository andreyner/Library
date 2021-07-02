using AutoMapper;
using DD.Library.Data;
using DD.Library.Model.Mappings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DD.Library.WebApplication.Controllers
{

	public class WardrobeListController : ControllerBase
	{
		public WardrobeListController(IMapper mapper)
		{
			this.AutoMapper = mapper;
			this.WardrobeListRepository = new WardrobeListRepository(mapper);
		}
		private readonly IMapper AutoMapper;
		private readonly WardrobeListRepository WardrobeListRepository;
		[HttpPost]
		public async Task<IActionResult> CreateWardrobeList([FromBody] List<WardrobeCreating> wardrobes)
		{
			Validator<WardrobeCreating>.CheckValid(wardrobes);
			var creatingTask = WardrobeListRepository.Create(wardrobes);
			await Task.WhenAll(creatingTask);
			return Ok();
		}
		[HttpGet]
		public async Task<IActionResult> GetWardrobes()
		{
			var gettingTask = WardrobeListRepository.GetAllWardrobes();
			await Task.WhenAll(gettingTask);
			return Ok(gettingTask.Result);
		}
		[HttpPut]
		public async Task<IActionResult> UpdateWardrobeList([FromBody] List<WardrobeUpdate> wardrobes)
		{
			Validator<WardrobeUpdate>.CheckValid(wardrobes);
			var updateingTask = WardrobeListRepository.Edit(wardrobes);
			await Task.WhenAll(updateingTask);
			return Ok();
		}
	}
}
