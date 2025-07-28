using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PP_PI_Backend.Data;
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

        [HttpGet]
        public async Task<List<Review>> Get()
        {
            return await context.Reviews.ToListAsync();
        }

        [HttpGet("{id:int}", Name = "GetReviewForId")]
        public async Task<ActionResult<Review>> Get(int id)
        {
            var review = await context.Reviews.FirstOrDefaultAsync(x => x.Id == id);
            if (review is null) 
            {
                return NotFound();
            }
            return review;
        }

        [HttpPost]
        public async Task<CreatedAtRouteResult> Post([FromBody] Review review)
        {
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
