﻿using bubanLibraryNowAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace bubanlibaryNowAPI.Models.Controllers
{
    [Route("api/v1/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static readonly List<Book> books = new List<Book>
        {
            new Book { Id = 1, Title = "Goodread", Author = "James Nap", Genre = "90s", Available = true, PublishedYear = 1990, Description = "" },
            new Book { Id = 2, Title = "HAS", Author = "ATHAN brex", Genre = "90s", Available = true, PublishedYear = 1995, Description = "" }
       };
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new { status = "success", data = books, message = "Books retrieve" });
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound(new { status = "error", data = (object?)null, message = "Books not found" });
            return Ok(new { status = "success", data = book, message = "Book retrieved" });
        }
        [HttpPost("{id}")]
        public IActionResult Create([FromBody] Book newbook)
        {
            newbook.Id = books.Count + 1;
            books.Add(newbook);
            return CreatedAtAction(nameof(GetById), new { id = newbook.Id },
                new { status = "success", data = books, message = "Book retrieved" });
        }
        [HttpPut]
        public IActionResult Update(int id, [FromBody] Book updateBook)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound(new { status = "error", data = (object?)null, messsage = "Book not found" });

            book.Title = updateBook.Title;
            book.Author = updateBook.Author;
            book.Genre = updateBook.Genre;
            book.Available = updateBook.Available;
            book.PublishedYear = updateBook.PublishedYear;

            return Ok(new { status = "success", data = books, messsage = "Book Update" });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = books.FirstOrDefault(book => book.Id == id);
            if (book == null)
                return NotFound(new { status = "error", data = (Object?)null, message = "Book not found" });

            books.Remove(book);
            return Ok(new { status = "success", data = (object?)null, message = "Books retrieved" });
        }
    }
}
