using AutoMapper;
using DD.Library.Data;
using DD.Library.Model;
using DD.Library.Model.Mappings;
using DD.Library.Model.Requests;
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
	public class BookController : ControllerBase
	{
		public BookController(IMapper mapper)
		{
			this.AutoMapper = mapper;
			this.BookRepository = new BookRepository(mapper);
		}
		private readonly IMapper AutoMapper;
		private readonly BookRepository BookRepository;
		/// <summary>
		/// Создание книги
		/// </summary>
		/// <remarks>
		/// {
		///"Name":"Сказки для детей",
		///"AuthorId":1,
		///"WardrobeId":1
		///}
		/// </remarks>
		/// <param name="book"></param>
		/// <returns></returns>
		[HttpPost(nameof(CreateBook))]
		public async Task<IActionResult> CreateBook([FromBody] BookCreating book)
		{
			Validator<BookCreating>.CheckValid(book);
			var bookDestination = AutoMapper.Map<Book>(book);
			var creatingTask = BookRepository.Create(bookDestination);
			await Task.WhenAll(creatingTask);
			return Ok();
		}
		/// <summary>
		/// Получение книги
		/// </summary>
		/// <remarks>
		/// 
		/// </remarks>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> GetBook(int id)
		{
			var gettingTask = BookRepository.GetById(id);
			await Task.WhenAll(gettingTask);
			var bookDestination = AutoMapper.Map<BookView>(gettingTask.Result);
			return Ok(bookDestination);
		}
		/// <summary>
		/// Обновление книги
		/// </summary>
		///<remarks>
		///{
		///"Id": 1,
		///"Name":"Book2",
		///"AuthorId": 50
		///}
		/// </remarks>
		/// <param name="book"></param>
		/// <returns></returns>
		[HttpPut(nameof(UpdateBook))]
		public async Task<IActionResult> UpdateBook([FromBody] BookUpdate book)
		{
			Validator<BookUpdate>.CheckValid(book);
			var updateingTask = BookRepository.Edit(book);
			await Task.WhenAll(updateingTask);
			return Ok();
		}
		/// <summary>
		/// Поместиить книгу в стилаж
		/// </summary>
		///<remarks>
		///{
		///"BookId": 1,
		///"WardrobeId": 2
		///}
		///</remarks>
		/// <param name="bookInWardrobeRequest"></param>
		/// <returns></returns>
		[HttpPut(nameof(PutBookInWardrobe))]
		public async Task<IActionResult> PutBookInWardrobe([FromBody] BookInWardrobe bookInWardrobeRequest)
		{
			Validator<BookInWardrobe>.CheckValid(bookInWardrobeRequest);
			var updateingTask = BookRepository.PutInWardrobe(bookInWardrobeRequest);
			await Task.WhenAll(updateingTask);
			return Ok();
		}
		/// <summary>
		/// Поиск книги по имени и по фамилии автора
		/// </summary>
		/// <remarks>
		/// {
		///"SearchText":"Тест"
		///}
		/// </remarks>
		/// <param name="searchText"></param>
		/// <returns></returns>
		[HttpPost(nameof(ExecuteFullTextSearch))]
		public async Task<IActionResult> ExecuteFullTextSearch([FromBody] FullSearch searchText)
		{
			Validator<FullSearch>.CheckValid(searchText);
			var searchTask = BookRepository.FullTextSearch(searchText);
			await Task.WhenAll(searchTask);
			return Ok(searchTask.Result);
		}
	}
}
