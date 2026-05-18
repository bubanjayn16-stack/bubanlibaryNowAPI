using bubanlibaryNowAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace bubanlibaryNowAPI.Controllers
{
    [Route("api/v1/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        // Static list acts as a temporary in-memory database
        private static readonly List<Book> books = new List<Book>
        {
            new Book { Id = 1, Title = "Theo Of Golden", Author = "Allen Levi", Genre = "Contemporary Fiction", Available = true, PublishedYear = 2025 },
            new Book { Id = 2, Title = "Theo", Author = "Levi", Genre = "Fiction", Available = true, PublishedYear = 2020 }
        };

        // GET: api/v1/books
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new { status = "success", data = books, message = "Books retrieved successfully" });
        }

        // GET: api/v1/books/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound(new { status = "error", data = (object?)null, message = "Book not found" });
            }

            return Ok(new { status = "success", data = book, message = "Book retrieved" });
        }

        // POST: api/v1/books
        [HttpPost]
        public IActionResult Create([FromBody] Book newBook)
        {
            newBook.Id = books.Any() ? books.Max(b => b.Id) + 1 : 1;
            books.Add(newBook);

            return CreatedAtAction(nameof(GetById), new { id = newBook.Id }, 
                new { status = "success", data = newBook, message = "Book created successfully" });
        }

        // PUT: api/v1/books/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Book updatedBook)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound(new { status = "error", data = (object?)null, message = "Book not found" });
            }

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Genre = updatedBook.Genre;
            book.Available = updatedBook.Available;
            book.PublishedYear = updatedBook.PublishedYear;

            return Ok(new { status = "success", data = book, message = "Book updated successfully" });
        }

        // DELETE: api/v1/books/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound(new { status = "error", data = (object?)null, message = "Book not found" });
            }

            books.Remove(book);
            return Ok(new { status = "success", data = (object?)null, message = "Book deleted successfully" });
        }
    }
}
