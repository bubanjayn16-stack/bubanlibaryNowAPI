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
        private static readonly object booksLock = new object();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new { status = "success", data = books, message = "Books" });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound(new { status = "error", data = (object?)null, message = "book not found" });

            return Ok(new { status = "success", data = book, message = "Book retrieved" });
        }

        [HttpPost]
        public IActionResult Create([FromBody] Book newBook)
        {
            if (newBook == null)
                return BadRequest(new { status = "error", data = (object?)null, message = "invalid book" });

            if (!ModelState.IsValid)
                return BadRequest(new { status = "error", data = ModelState, message = "validation failed" });

            lock (booksLock)
            {
                newBook.Id = books.Any() ? books.Max(b => b.Id) + 1 : 1;
                books.Add(newBook);
            }

            return CreatedAtAction(nameof(GetById), new { id = newBook.Id }, new { status = "success", data = newBook, message = "Book created" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Book updatedBook)
        {
            if (updatedBook == null)
                return BadRequest(new { status = "error", data = (object?)null, message = "invalid book" });

            if (!ModelState.IsValid)
                return BadRequest(new { status = "error", data = ModelState, message = "validation failed" });

            lock (booksLock)
            {
                var existing = books.FirstOrDefault(b => b.Id == id);
                if (existing == null)
                    return NotFound(new { status = "error", data = (object?)null, message = "book not found" });

                existing.Title = updatedBook.Title;
                existing.Description = updatedBook.Description;
                existing.Author = updatedBook.Author;
                existing.Genre = updatedBook.Genre;
                existing.Available = updatedBook.Available;
                existing.PublishedYear = updatedBook.PublishedYear;
            }

            return Ok(new { status = "success", data = updatedBook, message = "Book updated" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            lock (booksLock)
            {
                var book = books.FirstOrDefault(b => b.Id == id);
                if (book == null)
                    return NotFound(new { status = "error", data = (object?)null, message = "book not found" });

                books.Remove(book);
            }

            return NoContent();
        }
    }
}

