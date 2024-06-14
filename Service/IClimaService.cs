using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zefffood.Models;

namespace zefffood.Service
{
    public interface IClimaService
    {
         Task<Clima> ObtenerClimaPorCiudadAsync(string ciudad);
    }
}