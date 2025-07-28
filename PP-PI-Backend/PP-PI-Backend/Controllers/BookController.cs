using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PP_PI_Backend.Data;
using PP_PI_Backend.Models;

namespace PP_PI_Backend.Controllers
{
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        //private LibraryDb context;

        //public BookController(LibraryDb context)
        //{
        //    this.context = context;
        //}

        //[HttpGet] // Get a list of all books
        //public async Task<List<Book>> Get(){ return await context.Books.ToListAsync(); }


        //[HttpGet("{id:int}")] // Get a specific book by its ID
        //public async Task<ActionResult<Book>> Get(int id)
        //{
        //    var book = await context.Books.FirstOrDefaultAsync(x => x.Id == id); // Get from the DB the book that matches the ID
        //    if (book is null) { return NotFound(); } // If there's no found, it returns a 404 Not Found
        //    return book; // If not, then return the book. 
        //}

        //[HttpPost] // Insert a new book
        //public async Task<CreatedAtRouteResult> Post(Book book)
        //{
        //    context.Add(book);

        //}
    }
}
