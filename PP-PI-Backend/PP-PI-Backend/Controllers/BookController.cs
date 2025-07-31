using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PP_PI_Backend.Data;
using PP_PI_Backend.Models;

namespace PP_PI_Backend.Controllers
{
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private LibraryDb context;

        public BookController(LibraryDb context)
        {
            this.context = context;
        }

        [HttpGet] // Get a list of all books
        public async Task<List<Book>> Get() { return await context.Books.ToListAsync(); }


        [HttpGet("{id:int}")] // Get a specific book by its ID
        public async Task<ActionResult<Book>> Get(int id)
        {
            var book = await context.Books.FirstOrDefaultAsync(x => x.Id == id); // Get from the DB the book that matches the ID
            if (book is null) { return NotFound(); } // If there's no found, it returns a 404 Not Found
            return book; // If not, then return the book. 
        }

        [HttpPost] // Insert a new book
        public async Task<CreatedAtRouteResult> Post(Book book)
        {
            context.Add(book); // Stashes the insertion of the new register (memory operation)
            await context.SaveChangesAsync(); // Persists the insertion in the db table
            return CreatedAtRoute("GetAuthorById", new { id = book.Id }, book);
            // Returns the created book with the Get method, according to its new ID
        }

        [HttpPut("{id:int}")] // Modify a book
        public async Task<ActionResult> Put(int id, [FromBody] Book book)
        {
            var bookExists = await context.Books.AnyAsync(x => x.Id == id); // Generates a flag for the book search
            if (!bookExists) { return NotFound(); } // If there's no match, then returns a NotFound 
            context.Update(book); // Memory Operation
            await context.SaveChangesAsync(); // Persistance
            return NoContent(); // When the update has been finished correctly, the method returns a 204 No Content
        }

        [HttpDelete("{id:int}")] // Delete a book
        public async Task<ActionResult> Delete(int id)
        {
            var deletedRows = await context.Books.Where(x => x.Id == id).ExecuteDeleteAsync(); // Counts the amount of deleted rows
            if (deletedRows == 0) { return NotFound(); } // If there are no deleted rows, then the book wasn't found and deleted
            return NoContent(); // Else, return a 204 No Content
        }
    }
}
