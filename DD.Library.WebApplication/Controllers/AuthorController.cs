using AutoMapper;
using DD.Library.Data;
using DD.Library.IData;
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
	[Route("[controller]")]
	[ApiController]
	public class AuthorController : ControllerBase
	{
		public AuthorController(IMapper mapper, IAuthorRepository authorRepository)
		{
			this.AutoMapper = mapper;
			this.AuthorRepository =authorRepository;
		}
		private readonly IMapper AutoMapper;
		private readonly IAuthorRepository AuthorRepository;
		/// <summary>
		/// Создание автора
		/// </summary>
		/// <remarks>
		///{
		///	"FirstName":"Александр",
		///	"LastName":"",
		///	"Patronymic":"Пушкин"
		///}
		/// </remarks>
		/// <param name="author"></param>
		/// <returns></returns>
		[HttpPost(nameof(CreateAuthor))]
		public async Task<IActionResult> CreateAuthor([FromBody] AuthorCreating author)
		{
			Validator<AuthorCreating>.CheckValid(author);
			var authorDestination = AutoMapper.Map<Author>(author);
			var creatingTask = AuthorRepository.Create(authorDestination);
			await Task.WhenAll(creatingTask);
			return Ok();
		}
	}
}
