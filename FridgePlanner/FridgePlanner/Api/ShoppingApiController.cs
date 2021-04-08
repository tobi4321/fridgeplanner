using FridgePlanner.Entities;
using FridgePlanner.Repository;
using FridgePlanner.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgePlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingApiController : ShoppingBaseController<ShoppingItem, ShoppingItemRepository>
    {
        private readonly ShoppingItemRepository _repository;
        public ShoppingApiController(ShoppingItemRepository repository) : base(repository)
        {
            _repository = repository;
        }

        // POST: api/[controller]/Cart
        [HttpPost("Cart")]
        public async Task<ActionResult<Recipe>> AddToCart(Recipe addToCart)
        {
            List<ShoppingItem> shopping_items = await _repository.GetAll();

            for (int i = 0; i < addToCart.RecipeItems.Count; i++)
            {
                bool alreadyExists = false;
                ShoppingItem shoppingItem;
                for (int j = 0; j < shopping_items.Count; j++)
                {
                    if (addToCart.RecipeItems.ElementAt(i).Name.Equals(shopping_items.ElementAt(j).Name))
                    {
                        alreadyExists = true;
                        shoppingItem = shopping_items.ElementAt(j);
                        if (addToCart.RecipeItems.ElementAt(i).Unit.Equals(shopping_items.ElementAt(j).Unit))
                        {
                            shoppingItem.Amount = shoppingItem.Amount + addToCart.RecipeItems.ElementAt(i).Amount;
                        }
                        else
                        {
                            shoppingItem.Amount = shoppingItem.Amount + (double)UnitParser.ParseToUnit(shoppingItem.Unit, addToCart.RecipeItems.ElementAt(i).Amount);
                        }
                        j = shopping_items.Count;

                        await _repository.Update(shoppingItem);
                    }
                }
                if (!alreadyExists)
                {
                    ShoppingItem newItem = new ShoppingItem() { Name = addToCart.RecipeItems.ElementAt(i).Name, Amount = addToCart.RecipeItems.ElementAt(i).Amount, Unit = addToCart.RecipeItems.ElementAt(i).Unit };
                    await _repository.Add(newItem);
                }
            }
            
            return addToCart;
        }
    }
}
