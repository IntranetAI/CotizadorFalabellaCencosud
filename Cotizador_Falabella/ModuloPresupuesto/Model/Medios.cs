using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cotizador_Falabella.ModuloPresupuesto.Model
{
    public class Medios
    {
        public int ID_Medios { get; set; }
        public double ProcentajeMedios { get; set; }
        public int Cant_Santiago { get; set; }
        public int Cant_Regiones { get; set; }
        public int Cant_Stg_abc1 { get; set; }
        public int Cant_Stg_c2c3 { get; set; }
    }
}