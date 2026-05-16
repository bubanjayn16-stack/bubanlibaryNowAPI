using bubanlibaryNowAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace bubanlibaryNowAPI.Controllers
{
    [Route("api/v1/books")]
    [ApiController]

    public class bubanlibaryNowAPI : ControllerBase
    {
        private static List<Book> books = new List<Book>
       {
           new Book { Id = 1, Title = "Theo Of Golden" , Author = "Allen Levi", Genre = "Contemporary Fiction" ,Available = true, PublishedYear = 2025 },
           new Book { Id = 2, Title = "Theo " , Author = " Levi", Genre = " Fiction" ,Available = true, PublishedYear = 2020 }

       };

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new { status = "sucess", data = books, message = "Books retrieved" });
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)

                return NotFound(new { status = "error", data = (object?)null, message = "s not found" });
            return Ok(new { status = "success", data = books, message = "Books Retrieved" });

        }
        [HttpPost("{id}")]
        public IActionResult Create([FromBody] Book newbook)
        {
            newbook.Id = books.Count + 1;
            books.Add(newbook);
            return CreatedAtAction(nameof(GetById), new { id = newbook.Id },
            new { status = "success", data = books, message = "Books retrieved" });
        }
        [HttpPut]
        public IActionResult Update(int id, [FromBody] Book Updatebook)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound(new { status = "error", data = (object?)null, messagae = "s not found." });
            book.Title = Updatebook.Title;
            book.Author = Updatebook.Author;
            book.Genre = Updatebook.Genre;
            book.Available = Updatebook.Available;
            book.PublishedYear = Updatebook.PublishedYear;
            return Ok(new { status = "success", data = books, message = "Books retrieved" });

        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = books.FirstOrDefault(book => book.Id == id);
            if (book == null)
                return NotFound(new { status = "error", data = (object?)null, messagae = "s not found." });
            books.Remove(book);
            return Ok(new { status = "success", data = books, message = "Books retrieved" });


        }
    }
}
