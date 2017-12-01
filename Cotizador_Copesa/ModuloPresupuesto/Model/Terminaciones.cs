using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cotizador_Copesa.ModuloPresupuesto.Model
{
    public class Terminaciones
    {
        public string NombreTerminacion { get; set; }
        public double CostoFijo { get; set; }
        public double CostoVari { get; set; }
        public string Doblez { get; set; }
        public int Estado { get; set; }
        public string TipoTerm { get; set; }
    }
}