using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Razor.Api.Services.V1.Services;
using Razor.Api.Web.Filters;
using System.Linq;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Razor.Api.Web.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [Authorize]
    [ServiceFilter(typeof(AccessFilterAttribute))]
    [ApiController]
    public class KitsController : ControllerBase
    {
        private readonly IKitsService _kitsServices;
        public KitsController(IKitsService kitsServices)
        {
            _kitsServices = kitsServices;
        }

        [HttpGet("{uid}")] // also would be good to change the url from "/api/v1/Kits/Kit/{uid}" to "/api/v1/Kits/{uid}"
        [MapToApiVersion("1")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult GetKit([FromRoute][Required] string uid)
        {
            var kit = _kitsServices.GetKitByUid(uid);
            if(kit == null)
            {
                return NotFound();
            }
            return Ok(kit);
        }
             // here it's ok - we pull the information about the kit

        
        [MapToApiVersion("1")]
        [HttpGet("EnclosedInventory/{kitUid}")] //"/api/v1/Kits/EnclosedInventory/{uid}" is ok
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult GetEnclosedKitInventory([FromRoute][Required] string kitUid)
        {
            var inventory = _kitsServices.GetEnclosedKitInventory(kitUid);
            if (!inventory.Any())
            {
                return NotFound();
            }
            return Ok(inventory);
        }
        // TODO: here we should pull inventory parts enclosed into the kit by kit id
    }
}
