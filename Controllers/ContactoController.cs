using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using zefffood.Models;
using zefffood.Data;
using Microsoft.Extensions.ML;
using SentimentAnalysis;

namespace zefffood.Controllers
{
    public class ContactoController : Controller
    {
        private readonly ILogger<ContactoController> _logger;
        private readonly ApplicationDbContext _context;

         private readonly PredictionEnginePool<MLModel1.ModelInput, MLModel1.ModelOutput> _predictionEnginePool;

        public ContactoController(ILogger<ContactoController> logger,ApplicationDbContext context,PredictionEnginePool<MLModel1.ModelInput, MLModel1.ModelOutput> predictionEnginePool)
        {
            _logger = logger;
            _context = context;
            _predictionEnginePool = predictionEnginePool;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
       public IActionResult EnviarMensaje(Contacto objcontato)
        {
            _logger.LogDebug("Mensaje recibido");

            // Realizar predicción de sentimiento
            MLModel1.ModelInput modelInput = new MLModel1.ModelInput()
            {
                SentimentText = objcontato.Message
            };

            MLModel1.ModelOutput prediction = _predictionEnginePool.Predict(modelInput);

            // Determinar si el sentimiento es positivo o negativo basado en el score
            string sentimientoPredicho = prediction.Score[1] >= 0.5 ? "Positivo" : "Negativo";

            // Asignar el sentimiento predicho a la propiedad Text
            objcontato.Text = sentimientoPredicho;

            // Registrar los datos del objeto en la base de datos
            _context.Add(objcontato);
            _context.SaveChanges();

            ViewData["Message"] = "Se registró el contacto";
            ViewData["Sentimiento"] = prediction.PredictedLabel;
            ViewData["Score"] = prediction.Score[1];
            ViewData["SentimientoPredicho"] = sentimientoPredicho;

            ModelState.Clear();

            return View("Index");
        }

                        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}