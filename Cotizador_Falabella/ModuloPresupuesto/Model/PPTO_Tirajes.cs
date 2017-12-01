using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cotizador_Falabella.ModuloUsuario.Model;

namespace Cotizador_Falabella.ModuloPresupuesto.Model
{
    public class PPTO_Tirajes
    {
        public int IDTiraje { get; set; }
        public string NombreTiraje { get; set; }
        public string TirajeNombreExtendido { get; set; }
        public int Cantidad { get; set; }
        public double CostoTotal { get; set; }
        public double Costounitario { get; set; }
        public int Millaradicional { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public Usuario UsuarioCreador { get; set; }
    }
}