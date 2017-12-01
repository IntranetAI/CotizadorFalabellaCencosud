using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cotizador_Copesa.ModuloUsuario.Model;

namespace Cotizador_Copesa.ModuloPresupuesto.Model
{
    public class Cotizador
    {
        public int ID_Presupuesto { get; set; }
        public string NombrePresupuesto { get; set; }
        public string Formato { get; set; }
        public int Tiraje { get; set; }
        
        public int PaginasInt { get; set; }
        public int PaginasTap { get; set; }

        public string PapelInterior { get; set; }
        public string PapelTap { get; set; }

        public string GramajeInterior { get; set; }
        public string GramajeTapas { get; set; }

        public string MaquinaInterior { get; set; }
        public string MaquinaTap { get; set; }

        public string BarnizAcuosoInt { get; set; }
        public string BarnizAcuosoTap { get; set; }
        public string Encuadernacion { get; set; }
        public string QuintoColor { get; set; }
        public string BarnizUV { get; set; }
        public string Laminado { get; set; }

        public int TotalNeto { get; set; }
        public double PrecioUnitario { get; set; }

        public DateTime FechaCreacion { get; set; }
        public string Usuario_Creador { get; set; }
        public Usuario PersonalComercial { get; set; }

        public string Empresa { get; set; }
        public int EstadoPPTO { get; set; }
        public string Segmentos { get; set; }
        public int EntradasxFormatos { get; set; }

        

        public ValorUF ValorUFActual { get; set; }
    }
}