using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cotizador_Falabella.ModuloPresupuesto.Model
{
    public class Terminaciones
    {
        public string NombreTerminacion { get; set; }
        public string NombreSumplifacado { get; set; }
        public int Estado { get; set; }
        public string TipoCosto { get; set; }
        public double Costo { get; set; }
    }
}