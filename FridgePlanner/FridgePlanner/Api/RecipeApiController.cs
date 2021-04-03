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
    }
}
