using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cotizador_Copesa.ModuloUsuario.Controller;
using System.Web.Services;
using Cotizador_Copesa.ModuloUsuario.Model;

namespace Cotizador_Copesa.ModuloUsuario.View
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        [WebMethod]
        public static string Iniciar(string password, string Correo)
        {
            Controller_Usuario controluser = new Controller_Usuario();
            Usuario user = controluser.IniciarSession(Correo, password);
            if (user.NombreCompleto != "" && user.NombreCompleto != null)
            {
                return user.IDUsuario.ToString();
            }
            else
            {
                return "Error";
            }
        }
    }
}