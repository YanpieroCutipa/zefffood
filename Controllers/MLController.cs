using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.ML;
using Microsoft.Extensions.ML;
using SentimentAnalysis;

namespace zefffood.Controllers
{
   
    public class MLController : Controller
    {
        private readonly ILogger<MLController> _logger;
        private readonly PredictionEnginePool<MLModel1.ModelInput, MLModel1.ModelOutput> _predictionEnginePool;


        public MLController(ILogger<MLController> logger,
            PredictionEnginePool<MLModel1.ModelInput, MLModel1.ModelOutput> predictionEnginePool)
        {
            _logger = logger;
            _predictionEnginePool = predictionEnginePool;
        }

        public IActionResult Index()
        {
            return View();
        }

       public IActionResult Predict(String sentimiento)
{
    MLModel1.ModelInput modelInput = new MLModel1.ModelInput()
    {
        SentimentText = sentimiento
    };

    MLModel1.ModelOutput prediction = _predictionEnginePool.Predict(modelInput);

    // Determinar si el sentimiento es positivo o negativo basado en el score
    string sentimientoPredicho;
    if (prediction.Score[1] >= 0.5) // Umbral de 0.5 (ajustable según tus necesidades)
    {
        sentimientoPredicho = "Positivo";
    }
    else
    {
        sentimientoPredicho = "Negativo";
    }

    ViewData["Sentimiento"] = prediction.PredictedLabel;
    ViewData["Score"] = prediction.Score[1];
    ViewData["SentimientoPredicho"] = sentimientoPredicho;

    return View("Index");
}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}