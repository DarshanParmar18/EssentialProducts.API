using EssentialProducts.Core;
using EssentialProducts.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EssentialProducts.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly EssentialProductsDbContext dbContext;

        public CategoryController(EssentialProductsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var categories = await dbContext.Category.ToListAsync();
            return Ok(categories);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var category = await dbContext.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // POST api/<CategoryController>
        [HttpPost("", Name = "CreateCategory")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post([FromBody] string value)
        {
            var entity = new Category() { Name = value, IsActive=true };
            await dbContext.Category.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return new CreatedResult("GET", entity.Id);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] string value)
        {
            var entityInDb = await dbContext.Category.FindAsync(id);
            if (entityInDb == null)
            {
                return NotFound();
            }
            entityInDb.Name = value;
            dbContext.Update(entityInDb);
            await dbContext.SaveChangesAsync();
            return Ok(entityInDb);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entityInDb = await dbContext.Category.FindAsync(id);
            if (entityInDb == null)
            {
                return NotFound();
            }
            dbContext.Remove(entityInDb);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
