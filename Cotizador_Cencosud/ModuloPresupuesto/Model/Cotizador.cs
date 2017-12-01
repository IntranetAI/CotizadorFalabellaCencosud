using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cotizador_Cencosud.ModuloUsuario.Modelo;

namespace Cotizador_Cencosud.ModuloPresupuesto.Model
{
    public class Cotizador
    {
        public int ID_Presupuesto { get; set; }
        public string NombrePresupuesto { get; set; }
        public string Formato { get; set; }

        //oferta comercial
        public int PaginasInt { get; set; }
        public int PaginasTap { get; set; }

        public string PapelInterior { get; set; }
        public string PapelTap { get; set; }

        public string GramajeInterior { get; set; }
        public string GramajeTapas { get; set; }

        public string MaquinaInterior { get; set; }
        public string MaquinaTap { get; set; }

        public string Encuadernacion { get; set; }
        
        
        public DateTime FechaCreacion { get; set; }
        public string Usuario_Creador { get; set; }
        public Usuario PersonalComercial { get; set; }

        public string Empresa { get; set; }
        public int EstadoPPTO { get; set; }

        public PPTO_Tirajes Tiraje1 { get; set; }
        public PPTO_Tirajes Tiraje2 { get; set; }
        public PPTO_Tirajes Tiraje3 { get; set; }
        
        public ValorDolar_Trimestral ValorDolar { get; set; }
    }
}