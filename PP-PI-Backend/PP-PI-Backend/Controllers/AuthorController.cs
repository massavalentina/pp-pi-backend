using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PP_PI_Backend.Data;
using PP_PI_Backend.Models;

namespace PP_PI_Backend.Controllers
{

    [Route("api/authors")]
    public class AuthorController : ControllerBase

    {
        private LibraryDb context;

        public AuthorController(LibraryDb context)
        {
            this.context = context;
        }

        [HttpGet] // Get a list of all authors
        public async Task<List<Author>> Get()
        { return await context.Authors.ToListAsync(); }


        [HttpGet("{id:int}", Name = "GetAuthorById")] // Get a specific author by its ID
        public async Task<ActionResult<Author>> Get(int id)
        {
            var author = await context.Authors.FirstOrDefaultAsync(x => x.Id == id); // Get from the DB the author that matches the ID
            if (author is null) { return NotFound(); } // If there's no found, it returns a 404 Not Found
            return author; // If not, then return the author. 
        }

        [HttpPost] // Insert a new author
        public async Task<CreatedAtRouteResult> Post(Author author)
        {
            context.Add(author); // Stashes the insertion of the new register (memory operation)
            await context.SaveChangesAsync(); // Persists the insertion in the db table
            return CreatedAtRoute("GetAuthorById", new { id = author.Id }, author);
            // Returns the created author with the Get method, according to its new ID
        }

        [HttpPut("{id:int}")] // Modify an author
        public async Task<ActionResult> Put(int id,  Author author)
        {
            var authorExists = await context.Authors.AnyAsync(x => x.Id == id); // Generates a flag for the author search
            if (!authorExists) { return NotFound(); } // If there's no match, then returns a NotFound 
            context.Update(author); // Memory Operation
            await context.SaveChangesAsync(); // Persistance
            return NoContent(); // When the update has been finished correctly, the method returns a 204 No Content
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deletedRows = await context.Authors.Where(x=> x.Id == id).ExecuteDeleteAsync(); // Counts the amount of deleted rows
            if (deletedRows == 0) { return NotFound(); } // If there are no deleted rows, then the author wasn't found
            return NoContent(); // Else, return a 204 No Content
        }

    }

}
