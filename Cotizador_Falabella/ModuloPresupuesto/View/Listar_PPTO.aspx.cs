using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Cotizador_Falabella.ModuloPresupuesto.Controller;
using Cotizador_Falabella.ModuloPresupuesto.Model;

namespace Cotizador_Falabella.ModuloPresupuesto.View
{
    public partial class Listar_PPTO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string popupScript4 = "<script language='JavaScript'>muestraPPTOPendientes();muestraPPTOAprobados();</script>";
                Page.RegisterStartupScript("PopupScript", popupScript4);
            }
        }

        [WebMethod]
        public static string ListarPPTOPendientes(int EstadoPPTO, string Usuario)
        {
            Controller_Cotizador controlCot = new Controller_Cotizador();
            string arrayInserto = controlCot.ListarPPTO_Estado(EstadoPPTO,Usuario);
            return arrayInserto;
        }

        [WebMethod]
        public static string ListarPPTOAprobados(int EstadoPPTO, string Usuario)
        {
            Controller_Cotizador controlCot = new Controller_Cotizador();
            string arrayInserto = controlCot.ListarPPTO_Estado(EstadoPPTO, Usuario);
            return arrayInserto;
        }

        [WebMethod]
        public static string[] NombrePresupuesto(int id)
        {
            Controller_Cotizador controlCot = new Controller_Cotizador();
            Cotizador cot = controlCot.BuscarPresupuestoxID(id);

            return new[] { cot.NombrePresupuesto, cot.EstadoPPTO.ToString() };
        }

    }
}