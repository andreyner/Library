using AutoMapper;
using DD.Library.Data;
using DD.Library.Model.Mappings;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DD.Library.WebApplication.Controllers
{

	public class BookListController : ControllerBase
	{
		public BookListController(IMapper mapper)
		{
			this.AutoMapper = mapper;
			this.BookListRepository = new BookListRepository(mapper);
		}
		private readonly IMapper AutoMapper;
		private readonly BookListRepository BookListRepository;
		[HttpPost]
		public async Task<IActionResult> CreateBookList([FromBody] List<BookCreating> books)
		{
			Validator<BookCreating>.CheckValid(books);
			var creatingTask = BookListRepository.Create(books);
			await Task.WhenAll(creatingTask);
			return Ok();
		}
		[HttpGet]
		public async Task<IActionResult> GetBooks()
		{
			var gettingTask = BookListRepository.GetAllBooks();
			await Task.WhenAll(gettingTask);
			return Ok(gettingTask.Result);
		}
		[HttpPut]
		public async Task<IActionResult> UpdateBookList([FromBody] List<BookUpdate> books)
		{
			Validator<BookUpdate>.CheckValid(books);
			var updateingTask = BookListRepository.Edit(books);
			await Task.WhenAll(updateingTask);
			return Ok();
		}
	}
}
