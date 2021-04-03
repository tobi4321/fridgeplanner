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
            var shoppingItem = await repository.Get(id);
            if (shoppingItem == null)
            {
                return NotFound();
            }
            return shoppingItem;
        }

        // PUT: api/[controller]/5
        [HttpPut("{id}")]
        public async Task<ActionResult<TEntity>> Put(int id, TEntity shoppingItem)
        {
            if (id != shoppingItem.Id)
            {
                return BadRequest();
            }
            await repository.Update(shoppingItem);
            return shoppingItem;
        }

        // POST: api/[controller]
        [HttpPost]
        public async Task<ActionResult<TEntity>> Post(TEntity shoppingItem)
        {
            await repository.Add(shoppingItem);
            return CreatedAtAction("Get", new { id = shoppingItem.Id }, shoppingItem);
        }

        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TEntity>> Delete(int id)
        {
            var shoppingItem = await repository.Delete(id);
            if (shoppingItem == null)
            {
                return NotFound();
            }
            return shoppingItem;
        }
    }
}
