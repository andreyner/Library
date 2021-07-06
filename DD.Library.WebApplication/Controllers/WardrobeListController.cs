using AutoMapper;
using DD.Library.Data;
using DD.Library.IData;
using DD.Library.Model.Mappings;
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

	public class WardrobeListController : ControllerBase
	{
		public WardrobeListController(IMapper mapper, IWardrobeListRepository wardrobeListRepository)
		{
			this.AutoMapper = mapper;
			this.WardrobeListRepository = wardrobeListRepository;
		}
		private readonly IMapper AutoMapper;
		private readonly IWardrobeListRepository WardrobeListRepository;
		/// <summary>
		/// Создание списка стилажей
		/// </summary>
		/// <remarks>
		/// [
		///{
		///	"Name":"Стилаж1"
		///},
		///	{
		///	"Name":"Стилаж2"
		///}
		///]
		/// </remarks>
		/// <param name="wardrobes"></param>
		/// <returns></returns>
		[HttpPost(nameof(CreateWardrobeList))]
		public async Task<IActionResult> CreateWardrobeList([FromBody] List<WardrobeCreating> wardrobes)
		{
			Validator<WardrobeCreating>.CheckValid(wardrobes);
			var creatingTask = WardrobeListRepository.Create(wardrobes);
			await Task.WhenAll(creatingTask);
			return Ok();
		}
		/// <summary>
		/// Получение списка стилажей
		/// </summary>
		/// <returns></returns>
		[HttpGet(nameof(GetWardrobes))]
		public async Task<IActionResult> GetWardrobes()
		{
			var gettingTask = WardrobeListRepository.GetAllWardrobes();
			await Task.WhenAll(gettingTask);
			return Ok(gettingTask.Result);
		}
		/// <summary>
		/// Обновление списка стилажей
		/// </summary>
		/// <remarks>
		///	[
		///	{
		///	"Id": 1,
		///	"Name":"Стилаж1"
		///},
		///	{
		///	"Id": 50,
		///	"Name":"Стилаж2"
		///}
		///]
		/// </remarks>
		/// <param name="wardrobes"></param>
		/// <returns></returns>
		[HttpPut(nameof(UpdateWardrobeList))]
		public async Task<IActionResult> UpdateWardrobeList([FromBody] List<WardrobeUpdate> wardrobes)
		{
			Validator<WardrobeUpdate>.CheckValid(wardrobes);
			var updateingTask = WardrobeListRepository.Edit(wardrobes);
			await Task.WhenAll(updateingTask);
			return Ok();
		}
	}
}
