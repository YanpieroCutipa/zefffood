using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using zefffood.Models;
using zefffood.Service;
namespace zefffood.Controllers
{

    public class ClimaController : Controller
    {
        private readonly ILogger<ClimaController> _logger;
         private readonly IClimaService _climaService;


        public ClimaController(IClimaService climaService, ILogger<ClimaController> logger)
        {
            _logger = logger;
         _climaService = climaService;
        }

        public async Task<IActionResult> Index()
    {
         var clima = await _climaService.ObtenerClimaPorCiudadAsync("arequipa");
          
            return View("Clima/Index", clima);
    }

    }
}