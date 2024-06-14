using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using zefffood.Data;
using zefffood.Models;
using Microsoft.EntityFrameworkCore;
using zefffood.Service;

namespace zefffood.Controllers.Rest
{
    [ApiController]
    [Route("api/pago")]
     public class PagoApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
         private readonly PagoService _pagoService;
        public PagoApiController(ApplicationDbContext context,PagoService pagoService)
        {
            _context = context;
              _pagoService = pagoService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
       
        
        public async Task<ActionResult<List<Pago>>> List()
        {
            var pagos = await _pagoService.GetAll();
            if(pagos == null)
                return NotFound();
            return Ok(pagos);
        }

    }
}