using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cotizador_Falabella.ModuloUsuario.Controller;
using Cotizador_Falabella.ModuloUsuario.Model;

namespace Cotizador_Falabella.Estructura.MasterView
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
                else if (user.Empresa.ToUpper() == "FALABELLA")
                {
                    Image1.ImageUrl = "~/Estructura/Image/Logo_Falabella.png";
                }
                else if (user.Empresa.ToUpper() == "TOTTUS")
                {
                    Image1.ImageUrl = "~/Estructura/Image/Logo_Tottus.png";
                }
                else
                {
                    Image1.ImageUrl = "~/Estructura/Image/Logo_Sodimac.png";
                }
            }
            else
            {
                Response.Redirect("../../index.aspx");
            }
        }

        public void CrearMenu(int id)
        {
            //Controller_Usuario userControl = new Controller_Usuario();
            //string Empresa = Session["Empresa"].ToString();
            //lblUsuario.Text = Session["Usuario"].ToString();
            //if (Empresa == "Tottus")
            //{
            //    Image2.ImageUrl = "../Images/Tottus.png";
            //}
            //else if (Empresa == "Falabella")
            //{
            //    Image2.ImageUrl = "../Images/falabella.png";
            //}
            //else if (Empresa == "Homecenter")
            //{
            //    Image2.ImageUrl = "../Images/homecenter.png";
            //}
            //else
            //{
            //    Image2.ImageUrl = "../Images/Logo color lateral.jpg";
            //}
            //Usuario user = userControl.BuscarUsuario(Session["Alias"].ToString());
            //Session["TipoDetalle"] = user.Tipo_Detalle;
            //Session["Update"] = user.Permiso_Update;

            string Insert = "";
            //if (user.Permiso_Insert == 1)
            //{
            Insert = "<li><a href='../../ModuloPresupuesto/View/Presupuestador.aspx'>Generar Presupuesto</a></li>";
            //}
            string Cargo = "";
            //if (user.Cargo == 3 && user.Tipo_Detalle == 2)
            //{
            Cargo = "<li class='topmenu'><a href='#' style='height: 18px; line-height: 18px;'>Administración</a>" +
                    "<ul>" +
                        "<li><a href='../../ModuloUsuario/View/Admin_User.aspx'>Usuarios</a></li>" +
                        "<li><a href='../../ModuloPresupuesto/View/Mantenedor_Papeles.aspx'>PPTOs Históricos</a></li>" +
                    "</ul>" +
                "</li>";
            //}
            //Label2.Text = "<ul id='css3menu1' class='topmenu' style='width: 100%; margin-left: 0px;'> " +
            //    "<li class='topfirst'><a href='../../ModuloPresupuesto/View/Historico_PPTO.aspx' style='height: 18px;line-height: 18px;'>Inicio</a></li>" +
            //    "<li class='topmenu'><a href='#' style='height: 18px; line-height: 18px;'>Presupuesto</a>" +
            //        "<ul>" +
            //            Insert +
            //            "<li><a href='../../ModuloPresupuesto/View/Historico_PPTO.aspx'>PPTOs Históricos</a></li>" +
            //        "</ul>" +
            //    "</li>" +
            //    Cargo +
            //    "<li class='toplast'><a href='../../ModuloPresupuesto/View/Contacto.aspx' style='height: 18px;line-height: 18px;'>Contactos</a></li>" +
            //    "<li class='toplast'><a href='../../ModuloPresupuesto/View/Sugerencias.aspx' style='height: 18px; line-height: 18px;'>Sugerencias</a></li>" +
            //"</ul>";
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