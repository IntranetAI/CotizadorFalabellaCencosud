using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cotizador_Copesa.ModuloUsuario.Controller;
using Cotizador_Copesa.ModuloUsuario.Model;

namespace Cotizador_Copesa.Estructura.MasterView
{
    public partial class MasterEstructura : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    int id = Convert.ToInt32(Request.QueryString["id"].ToString());
                    CargarUsuario(id);
                    CrearMenu(id);
                }
                catch
                {
                    Response.Redirect("../../index.aspx");
                }
            }
        }

        public void CargarUsuario(int id)
        {
            Controller_Usuario controlu = new Controller_Usuario();
            Usuario user = controlu.BuscarUsuario_ID(id);
            if (user.NombreCompleto != null)
            {
                lblUsuario.Text = user.NombreCompleto;

                Image1.AlternateText = user.Empresa;
                if (user.Empresa.ToUpper() == "AIMPRESORES")
                {
                    Image1.ImageUrl = "~/Estructura/Image/LOGO%20A.png";
                    Image1.Width = 200;
                }
                else
                {
                    Image1.ImageUrl = "~/Estructura/Image/logo_copesa.jpg";
                }
            }
            else
            {
                Response.Redirect("../../index.aspx");
            }
        }

        public void CrearMenu(int id)
        {
            string Insert = "";
            Insert = "<li><a href='../../ModuloPresupuesto/View/Presupuestador.aspx'>Generar Presupuesto</a></li>";

            string Cargo = "";
            Cargo = "<li class='topmenu'><a href='#' style='height: 18px; line-height: 18px;'>Administración</a>" +
                    "<ul>" +
                        "<li><a href='../../ModuloUsuario/View/Admin_User.aspx'>Usuarios</a></li>" +
                        "<li><a href='../../ModuloPresupuesto/View/Mantenedor_Papeles.aspx'>PPTOs Históricos</a></li>" +
                    "</ul>" +
                "</li>";
            Label2.Text = "<nav class='navbar navbar-default' role='navigation'>" +
                            "<div class='container-fluid'>" +
                            "<div class='navbar-header'>" +
                              "<a class='navbar-brand' href='#'>Cotizador</a>" +
                            "</div>" +
                            "<ul class='nav navbar-nav'>" +
                              "<li class='active'><a href='#'>Inicio</a></li>" +
                              "<li><a href='../../ModuloPresupuesto/View/Presupuestador.aspx?id=" + id + "'>Presupuesto</a></li>" +
                              "<li><a href='../../ModuloPresupuesto/View/Listar_PPTO.aspx?id=" + id + "'>Historial PPTO</a></li>" +
                              "<li><a href='../../ModuloUsuario/View/Contactos_Comerciales.aspx?id=" + id + "'>Contactos</a></li>" +
                              "<li><a href='#'>Sugerencias</a></li>" +
                            "</ul>" +
                            "</div>" +
                            "</nav>";
        }
    }
}