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
    public class RecipeBaseController : ControllerBase
    {
        private readonly IRepositoryWrapper _recipeWrapper;

        public RecipeBaseController(IRepositoryWrapper repoWrapper)
        {
            _recipeWrapper = repoWrapper;
        }

        // GET: api/[controller]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> Get()
        {
            return await _recipeWrapper.Recipes.GetAll();
        }

        // GET: api/[controller]/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> Get(int id)
        {
            var recipe = await _recipeWrapper.Recipes.Get(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return recipe;
        }

        // PUT: api/[controller]/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return BadRequest();
            }
            await _recipeWrapper.Recipes.Update(recipe);
            return NoContent();
        }

        // POST: api/[controller]
        [HttpPost]
        public async Task<ActionResult<object>> Post(Recipe recipe)
        {
            await _recipeWrapper.Recipes.Add(recipe);
            return CreatedAtAction("Get", new { id = recipe.Id }, recipe);
        }

        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> Delete(int id)
        {
            var fridgeItem = await _recipeWrapper.Recipes.Delete(id);
            if (fridgeItem == null)
            {
                return NotFound();
            }
            return fridgeItem;
        }
    }
}
