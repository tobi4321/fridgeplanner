using FridgePlanner.Entities;
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
    public class RecipeApiController : RecipeBaseController<Recipe, RecipeRepository>
    {
        private readonly RecipeRepository _repository;
        public RecipeApiController(RecipeRepository repository) : base(repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public override async Task<ActionResult<IEnumerable<object>>> Get()
        {
            return await _repository.GetAll();
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<object>> Get(int id)
        {
            var recipe = await _repository.Get(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return recipe;
        }

        // PUT: api/[controller]/Item/5
        [HttpPut("Item/{id}")]
        public async Task<ActionResult<object>> PutItem(int id, RecipeItem item)
        {
            var recipe = await _repository.Get(id);
            if (id != recipe.Id)
            {
                return BadRequest();
            }
            await _repository.UpdateItem(recipe,item);
            return recipe;
        }

        // POST: api/[controller]/Item
        [HttpPost("Item/{id}")]
        public async Task<ActionResult<object>> PostItem(int id, RecipeItem item)
        {
            var recipe = await _repository.Get(id);
            recipe = await _repository.AddItem(recipe,item);
            return recipe;
        }

        // DELETE: api/[controller]/Item/5
        [HttpDelete("Item/{id}/{itemId}")]
        public async Task<ActionResult<object>> DeleteItem(int id, int itemId)
        {
            var recipe = await _repository.Get(id);
            var item = recipe.RecipeItems.Where(i => i.Id == itemId).First();
            recipe = await _repository.DeleteItem(recipe,item);
            if (recipe == null)
            {
                return NotFound();
            }
            return recipe;
        }

        // PUT: api/[controller]/Step/5
        [HttpPut("Step/{id}")]
        public async Task<ActionResult<object>> PutStep(int id, RecipeStep step)
        {
            var recipe = await _repository.Get(id);
            if (id != recipe.Id)
            {
                return BadRequest();
            }
            await _repository.UpdateStep(recipe, step);
            return recipe;
        }

        // POST: api/[controller]/Step
        [HttpPost("Step/{id}")]
        public async Task<ActionResult<object>> PostStep(int id, RecipeStep step)
        {
            var recipe = await _repository.Get(id);
            recipe = await _repository.AddStep(recipe, step);
            return recipe;
        }

        // DELETE: api/[controller]/Step/5
        [HttpDelete("Step/{id}/{stepId}")]
        public async Task<ActionResult<object>> DeleteStep(int id, int stepId)
        {
            var recipe = await _repository.Get(id);
            var step = recipe.RecipeSteps.Where(s => s.Id == stepId).First();
            recipe = await _repository.DeleteStep(recipe, step);
            if (recipe == null)
            {
                return NotFound();
            }
            return recipe;
        }
    }
}
