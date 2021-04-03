using FridgePlanner.Data;
using FridgePlanner.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeBaseController<TEntity, TRepository> : ControllerBase
        where TEntity : class, IEntity
        where TRepository : IRepository<TEntity>
    {
        private readonly TRepository _repository;

        public RecipeBaseController(TRepository repository)
        {
            _repository = repository;
        }

        // GET: api/[controller]
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<object>>> Get()
        {
            return await _repository.GetAll();
        }

        // GET: api/[controller]/5
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<object>> Get(int id)
        {
            var recipe = await _repository.Get(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return recipe;
        }

        // PUT: api/[controller]/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TEntity recipe)
        {
            if (id != recipe.Id)
            {
                return BadRequest();
            }
            await _repository.Update(recipe);
            return NoContent();
        }

        // POST: api/[controller]
        [HttpPost]
        public async Task<ActionResult<object>> Post(TEntity recipe)
        {
            await _repository.Add(recipe);
            return CreatedAtAction("Get", new { id = recipe.Id }, recipe);
        }

        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> Delete(int id)
        {
            var fridgeItem = await _repository.Delete(id);
            if (fridgeItem == null)
            {
                return NotFound();
            }
            return fridgeItem;
        }
    }
}
