using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cotizador_Falabella.ModuloPresupuesto.Model
{
    public class ValorDolar_Trimestral
    {
        public int idTrimestre { get; set; }
        public string NombreTrimestre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }
        public double ValorTrimestre { get; set; }
    }
}