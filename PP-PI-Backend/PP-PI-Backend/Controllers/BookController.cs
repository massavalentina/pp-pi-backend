using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PP_PI_Backend.Data;
using PP_PI_Backend.DTO.Book;
using PP_PI_Backend.Models;
using PP_PI_Backend.DTO.Review;

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
        public async Task<List<BookDTO>> GetAll() // Returns a list of the DTO's
        {
            return await context.Books // Brings the Book table
                .Include(b => b.Author) // JOINS with Author
                .Include(b => b.Publisher) // JOINS with Publisher
                .Select(b => new BookDTO
                {
                    Id = b.Id, // Inserts into the DTO all the information of every Book
                    Title = b.Title,
                    Description = b.Description,
                    PublicationDate = b.PublicationDate,
                    ImageUrl = b.ImageUrl,
                    AuthorName = b.Author.FirstName + " " + b.Author.LastName,
                    PublisherName = b.Publisher.Name
                }).ToListAsync();
        }

        [HttpGet("{id:int}")] // Get an specific books by its ID
        public async Task<ActionResult<BookWithReviewsDTO>> GetById(int id) // Returns a single instance of the DTO
        {
            var book = await context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.Reviews)
                .FirstOrDefaultAsync(b => b.Id == id); // If brings the book that matches the ID

            if (book == null) return NotFound();

            var dto = new BookWithReviewsDTO // Creates a new DTO and inserts all the necessary informations 
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                PublicationDate = book.PublicationDate,
                ImageUrl = book.ImageUrl,
                AuthorName = $"{book.Author.FirstName} {book.Author.LastName}",
                PublisherName = book.Publisher.Name,
                Reviews = book.Reviews.Select(r => new ReviewDTO
                {
                    Id = r.Id,
                    Comment = r.Comment,
                    Rating = r.Rating
                }).ToList()
            };
            return dto;
        }

        [HttpGet("forreviews")]
        public async Task<ActionResult<List<BookWithReviewsDTO>>> GetBooksForReviews()
        {
            var books = await context.Books
                .Include(b => b.Author)        // Incluye la relación con autor
                .Include(b => b.Publisher)     // Incluye la relación con editorial/publisher
                .Select(b => new BookWithReviewsDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    AuthorName = b.Author.FirstName,           // Asegurate que la propiedad se llame así
                    PublisherName = b.Publisher.Name      // Igual acá
                })
                .ToListAsync();

            return Ok(books);
        }

        [HttpPost] // Creates a new book
        public async Task<ActionResult<Book>> Post([FromBody] BookCreateDTO dto)
        {
            // Verifies if the author exists
            var authorExists = await context.Authors.AnyAsync(a => a.Id == dto.AuthorId);
            if (!authorExists)
            {
                return BadRequest($"No se encontró el autor con ID {dto.AuthorId}.");
            }

            // Verifies if the publishers exists
            var publisherExists = await context.Publishers.AnyAsync(p => p.Id == dto.PublisherId);
            if (!publisherExists)
            {
                return BadRequest($"No se encontró la editorial con ID {dto.PublisherId}.");
            }

            // Creates the book
            var book = new Book
            {
                Title = dto.Title,
                Description = dto.Description,
                PublicationDate = dto.PublicationDate,
                ImageUrl = dto.ImageUrl,
                AuthorId = dto.AuthorId,
                PublisherId = dto.PublisherId
            };

            context.Books.Add(book);
            await context.SaveChangesAsync();

            return CreatedAtRoute("GetById", new { id = book.Id }, book);
        }

        [HttpPut("{id}")] // Modifies a book by its ID
        public async Task<ActionResult> Put(int id, [FromBody] BookUpdateDTO dto)
        {
           
            var bookInDb = await context.Books.FirstOrDefaultAsync(b => b.Id == id); // Brings the book
            if (bookInDb == null)
            {
                return NotFound($"No se encontró el libro con ID {id}.");
            }

            // Verifying the relations
            var authorExists = await context.Authors.AnyAsync(a => a.Id == dto.AuthorId);
            var publisherExists = await context.Publishers.AnyAsync(p => p.Id == dto.PublisherId);

            if (!authorExists || !publisherExists)
            {
                return BadRequest("El autor o la editorial especificados no existen.");
            }

            // Updates the information
            bookInDb.Title = dto.Title;
            bookInDb.Description = dto.Description;
            bookInDb.PublicationDate = dto.PublicationDate;
            bookInDb.ImageUrl = dto.ImageUrl;
            bookInDb.AuthorId = dto.AuthorId;
            bookInDb.PublisherId = dto.PublisherId;

            await context.SaveChangesAsync();
            return NoContent(); // 204
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
