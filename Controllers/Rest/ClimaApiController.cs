using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using zefffood.Service;

namespace zefffood.Controllers.Rest
{
    [ApiController]
    [Route("api/clima")]
    public class ClimaApiController : ControllerBase
    {
         private readonly IClimaService _climaService;
           public ClimaApiController(IClimaService climaService)
        {
        
         _climaService = climaService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
         public async Task<IActionResult> GetClima(string ciudad)
    {
        var clima = await _climaService.ObtenerClimaPorCiudadAsync(ciudad);
        if (clima == null)
        {
            return NotFound();
        }
        return Ok(clima);
    }
    }
}