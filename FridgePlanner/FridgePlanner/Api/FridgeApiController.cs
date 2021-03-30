using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FridgePlanner.Data;
using FridgePlanner.EFCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FridgePlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FridgeApiController : FridgeBaseController<FridgeItem, EFCoreFridgeItemRepository>
    {
        public FridgeApiController(EFCoreFridgeItemRepository repository) : base(repository)
        {

        }
    }
}