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

        [HttpGet] // Gets all of the reviews
        public async Task<ActionResult<List<ReviewDTO>>> Get()
        {
            var reviews = await context.Reviews
                .Select(r => new ReviewDTO
                {
                    Id = r.Id,
                    Comment = r.Comment,
                    Rating = r.Rating
                })
                .ToListAsync();

            return reviews;
        }


        //[HttpGet]
        //public async Task<List<Review>> Get()
        //{
        //    return await context.Reviews.ToListAsync();
        //}

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


        //[HttpPost]
        //public async Task<CreatedAtRouteResult> Post([FromBody] Review review)
        //{
        //    context.Add(review);
        //    await context.SaveChangesAsync();

        //    return CreatedAtRoute("GetReviewForId", new { id = review.Id }, review);
        //}

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
