using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cotizador_Falabella.ModuloPresupuesto.Model
{
    public class Vehiculo
    {
        public int ID_Vehiculo { get; set; }
        public string NombreVehiculo { get; set; }
        public int Capacidad { get; set; }
        public int Costo_Flete { get; set; }
    }
}