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

	public class BookController : ControllerBase
	{
		public BookController(IMapper mapper)
		{
			this.AutoMapper = mapper;
			this.BookRepository = new BookRepository(mapper);
		}
		private readonly IMapper AutoMapper;
		private readonly BookRepository BookRepository;
		public string Index()
		{
			return $"Hello from Book!";
		}
		[HttpPost]
		public async Task<IActionResult> CreateBook([FromBody] BookCreating book)
		{
			Validator<BookCreating>.CheckValid(book);
			var bookDestination = AutoMapper.Map<Book>(book);
			var creatingTask = BookRepository.Create(bookDestination);
			await Task.WhenAll(creatingTask);
			return Ok();
		}
		[HttpGet]
		public async Task<IActionResult> GetBook(int id)
		{
			var gettingTask = BookRepository.GetById(id);
			await Task.WhenAll(gettingTask);
			var bookDestination = AutoMapper.Map<BookView>(gettingTask.Result);
			return Ok(bookDestination);
		}
		[HttpPut]
		public async Task<IActionResult> UpdateBook([FromBody] BookUpdate book)
		{
			Validator<BookUpdate>.CheckValid(book);
			var updateingTask = BookRepository.Edit(book);
			await Task.WhenAll(updateingTask);
			return Ok();
		}
		[HttpPut]
		public async Task<IActionResult> PutBookInWardrobe([FromBody] BookInWardrobe bookInWardrobeRequest)
		{
			Validator<BookInWardrobe>.CheckValid(bookInWardrobeRequest);
			var updateingTask = BookRepository.PutInWardrobe(bookInWardrobeRequest);
			await Task.WhenAll(updateingTask);
			return Ok();
		}
		[HttpPost]
		public async Task<IActionResult> ExecuteFullTextSearch([FromBody] FullSearch searchText)
		{
			Validator<FullSearch>.CheckValid(searchText);
			var searchTask = BookRepository.FullTextSearch(searchText);
			await Task.WhenAll(searchTask);
			return Ok(searchTask.Result);
		}
	}
}
