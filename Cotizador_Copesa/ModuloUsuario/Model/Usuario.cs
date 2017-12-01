using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cotizador_Copesa.ModuloUsuario.Model
{
    public class Usuario
    {
        public int IDUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string NombreUsuario { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Fax { get; set; }
        public string Correo { get; set; }
        public string Perfil { get; set; }
        public int Estado { get; set; }
        public string Empresa { get; set; }
    }
}