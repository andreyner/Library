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

	public class AuthorController : ControllerBase
	{
		public AuthorController(IMapper mapper)
		{
			this.AutoMapper = mapper;
			this.AuthorRepository = new AuthorRepository();
		}
		private readonly IMapper AutoMapper;
		private readonly AuthorRepository AuthorRepository;
		[HttpPost]
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
