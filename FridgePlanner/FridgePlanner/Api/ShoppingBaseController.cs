using FridgePlanner.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingBaseController<TEntity, TRepository> : ControllerBase
        where TEntity : class, IEntity
        where TRepository : IRepository<TEntity>
    {
        private readonly TRepository repository;

        public ShoppingBaseController(TRepository repository)
        {
            this.repository = repository;
        }


        // GET: api/[controller]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> Get()
        {
            return await repository.GetAll();
        }

        // GET: api/[controller]/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> Get(int id)
        {
            var fridgeItem = await repository.Get(id);
            if (fridgeItem == null)
            {
                return NotFound();
            }
            return fridgeItem;
        }

        // PUT: api/[controller]/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TEntity fridgeItem)
        {
            if (id != fridgeItem.Id)
            {
                return BadRequest();
            }
            await repository.Update(fridgeItem);
            return NoContent();
        }

        // POST: api/[controller]
        [HttpPost]
        public async Task<ActionResult<TEntity>> Post(TEntity fridgeItem)
        {
            await repository.Add(fridgeItem);
            return CreatedAtAction("Get", new { id = fridgeItem.Id }, fridgeItem);
        }

        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TEntity>> Delete(int id)
        {
            var fridgeItem = await repository.Delete(id);
            if (fridgeItem == null)
            {
                return NotFound();
            }
            return fridgeItem;
        }
    }
}
