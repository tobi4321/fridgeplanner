using FridgePlanner.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class FridgeBaseController<TEntity, TRepository> : ControllerBase
        where TEntity : class, IEntity
        where TRepository : IRepository<TEntity>
    {
        private readonly TRepository _repository;

        public FridgeBaseController(TRepository repository)
        {
            _repository = repository;
        }


        // GET: api/[controller]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> Get()
        {
            return await _repository.GetAll();
        }

        // GET: api/[controller]/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> Get(int id)
        {
            var fridgeItem = await _repository.Get(id);
            if (fridgeItem == null)
            {
                return NotFound();
            }
            return fridgeItem;
        }

        // PUT: api/[controller]/5
        [HttpPut("{id}")]
        public async Task<ActionResult<TEntity>> Put(int id, TEntity fridgeItem)
        {
            if (id != fridgeItem.Id)
            {
                return BadRequest();
            }
            await _repository.Update(fridgeItem);
            return fridgeItem;
        }

        // POST: api/[controller]
        [HttpPost]
        public async Task<ActionResult<TEntity>> Post(TEntity fridgeItem)
        {
            await _repository.Add(fridgeItem);
            return CreatedAtAction("Get", new { id = fridgeItem.Id }, fridgeItem);
        }

        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TEntity>> Delete(int id)
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
