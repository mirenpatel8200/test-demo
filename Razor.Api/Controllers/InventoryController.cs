using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Razor.Api.Services.V1.Services;
using Razor.Api.Web.Filters;

namespace Razor.Api.Web.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ServiceFilter(typeof(AccessFilterAttribute))]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryServices;

        public InventoryController(IInventoryService inventoryServices)
        {
            _inventoryServices = inventoryServices;
        }

        [MapToApiVersion("1")]
        [HttpGet("{uid}")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult GetInventory([FromRoute][Required] string uid)
        {
            var inventory = _inventoryServices.GetInventory(uid);
            if( inventory == null)
            {
                return NotFound();
            }
            return Ok(inventory);
        }
           
        // here we pull the inventory part by its uid without knowing about the kits

    }
}
