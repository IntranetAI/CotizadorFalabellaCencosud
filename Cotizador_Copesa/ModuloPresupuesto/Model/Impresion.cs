using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cotizador_Copesa.ModuloPresupuesto.Model
{
    public class Impresion
    {
        public int IdImpresion { get; set; }
        public string Paginas { get; set; }
        public string Maquina { get; set; }
        public double Costo { get; set; }
        public string TipoCosto { get; set; }

    }
}