using AutoMapper;
using DD.Library.Data;
using DD.Library.Model.Mappings;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DD.Library.WebApplication.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class BookListController : ControllerBase
	{
		public BookListController(IMapper mapper)
		{
			this.AutoMapper = mapper;
			this.BookListRepository = new BookListRepository(mapper);
		}
		private readonly IMapper AutoMapper;
		private readonly BookListRepository BookListRepository;
		/// <summary>
		/// Создание списка книг
		/// </summary>
		/// <remarks>
		///	[
		///	{
		///	"Name":"Book2",
		///	"AuthorId":1,
		///	"WardrobeId":1
		///},
		///	{
		///	"Name":"Book3",
		///	"AuthorId":1,
		///	"WardrobeId":1
		///}	
		///]
		/// </remarks>
		/// <param name="books"></param>
		/// <returns></returns>
		[HttpPost(nameof(CreateBookList))]
		public async Task<IActionResult> CreateBookList([FromBody] List<BookCreating> books)
		{
			Validator<BookCreating>.CheckValid(books);
			var creatingTask = BookListRepository.Create(books);
			await Task.WhenAll(creatingTask);
			return Ok();
		}
		/// <summary>
		/// Получение списка книг
		/// </summary>
		/// <remarks>
		/// 
		/// </remarks>
		/// <returns></returns>
		[HttpGet(nameof(GetBooks))]
		public async Task<IActionResult> GetBooks()
		{
			var gettingTask = BookListRepository.GetAllBooks();
			await Task.WhenAll(gettingTask);
			return Ok(gettingTask.Result);
		}
		/// <summary>
		/// Обновление списка книг
		/// </summary>
		/// <remarks>
		///	[
		///	{
		///	"id":1,
		///	"Name":"sxsxs4",
		///	"AuthorId":1
		///},
		///{
		///	"id":2,
		///	"Name":"sxsxs5",
		///	"AuthorId":2
		///}
		///]
		/// </remarks>
		/// <param name="books"></param>
		/// <returns></returns>
		[HttpPut(nameof(UpdateBookList))]
		public async Task<IActionResult> UpdateBookList([FromBody] List<BookUpdate> books)
		{
			Validator<BookUpdate>.CheckValid(books);
			var updateingTask = BookListRepository.Edit(books);
			await Task.WhenAll(updateingTask);
			return Ok();
		}
	}
}
