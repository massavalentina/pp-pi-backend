using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PP_PI_Backend.Data;
using PP_PI_Backend.DTO.Review;
using PP_PI_Backend.Models;


namespace PP_PI_Backend.Controllers
{
    [Route("api/reviews")]
    public class ReviewsController : ControllerBase
    {
        private readonly LibraryDb context;

        public ReviewsController(LibraryDb context)
        {
            this.context = context;
        }
        

        [HttpGet("{bookId}/reviews")]
        public async Task<ActionResult<IEnumerable<BookForReviewDTO>>> GetReviewsForBook(int bookId)
        {
            var reviews = await context.Reviews
                .Where(r => r.BookId == bookId)
                .Select(r => new BookForReviewDTO
                {
                    Id = r.Id,
                    BookId = r.BookId,
                    Title = r.Book.Title,
                    Comment = r.Comment,
                    Rating = r.Rating
                })
                .ToListAsync();

            return Ok(reviews);
        }

        

        [HttpGet("book/{bookId}")]
        public async Task<ActionResult<List<BookForReviewDTO>>> GetReviewsByBook(int bookId)
        {
            var reviews = await context.Reviews
                .Include(r => r.Book)
                .Where(r => r.BookId == bookId)
                .Select(r => new BookForReviewDTO
                {
                    Id = r.Id,
                    BookId = r.BookId,
                    Title = r.Book.Title,
                    Comment = r.Comment,
                    Rating = r.Rating
                })
                .ToListAsync();

            return reviews;
        }




        [HttpGet("{id:int}", Name = "GetReviewForId")] // Get a review by its ID
        public async Task<ActionResult<Review>> Get(int id)
        {
            var review = await context.Reviews.FirstOrDefaultAsync(x => x.Id == id);
            if (review is null) 
            {
                return NotFound();
            }
            return review;
        }

        [HttpPost] // Creates a new review for a specific book
        public async Task<ActionResult> Post([FromBody] ReviewCreateDTO reviewDto)
        {
            var review = new Review
            {
                BookId = reviewDto.BookId,
                Comment = reviewDto.Comment,
                Rating = reviewDto.Rating
            };

            context.Add(review);
            await context.SaveChangesAsync();

            return CreatedAtRoute("GetReviewForId", new { id = review.Id }, review);
        }


        

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id,[FromBody] Review review)
        {
            var existReview = await context.Reviews.AnyAsync(x => x.Id == id);

            if (!existReview)
            {
                return NotFound();
            }

            review.Id = id;

            context.Update(review);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deletedRows = await context.Reviews.Where(x => x.Id == id).ExecuteDeleteAsync();

            if (deletedRows == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
