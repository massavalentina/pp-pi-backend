using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PP_PI_Backend.Data;
using PP_PI_Backend.Entities;

namespace PP_PI_Backend.Controllers
{
    [Route("api/publishers")]
    public class PublishersController : ControllerBase
    {
        private readonly LibraryDb context;
        public PublishersController(LibraryDb context) 
        { 
            this.context = context;
        }

        [HttpGet]
        public async Task<List<Publisher>> Get()
        {
            return await context.Publishers.ToListAsync();
        }

        [HttpGet("{id:int}", Name = "GetById")]
        public async Task<ActionResult<Publisher>> Get(int id)
        {
            var publisher = await context.Publishers.FirstOrDefaultAsync(x => x.Id == id);

            if (publisher is null)
            {
                return NotFound();
            }

            return publisher;
        }

        [HttpPost]
        public async Task<CreatedAtRouteResult> Post([FromBody] Publisher publisher)
        {
            context.Add(publisher);
            await context.SaveChangesAsync();
            return CreatedAtRoute("GetById", new { id = publisher.Id }, publisher);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Publisher publisher)
        {
            var publisherExists = await context.Publishers.AnyAsync(x => x.Id == id);

            if (!publisherExists)
            {
                return NotFound();
            }

            publisher.Id = id;
            context.Update(publisher);
            await context.SaveChangesAsync();
            return NoContent();
        }



        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await context.Publishers.Where(x => x.Id == id).ExecuteDeleteAsync();

            if (deleted == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
