using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cotizador_Cencosud.ModuloPresupuesto.Model
{
    public class Tarifa_Papel
    {
        public double PrecioPapel { get; set; }
        public int TarifaMermaFija { get; set; }
        public double TarifaMermaVariable { get; set; }
    }
}