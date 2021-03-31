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
    public class RecipeApiController : RecipeBaseController
    {
        public RecipeApiController(IRepositoryWrapper repository) : base(repository)
        {

        }
    }
}
